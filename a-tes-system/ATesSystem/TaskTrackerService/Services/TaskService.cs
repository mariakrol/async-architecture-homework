using Microsoft.EntityFrameworkCore;
using TaskTrackerService.Data.RequestResponseModels.Task;
using TaskTrackerService.Data.Storage;

using Task = TaskTrackerService.Data.Storage.Task;

namespace TaskTrackerService.Services;

internal class TaskService : ITaskService
{
    private readonly IWorkerSelectionService _workerSelectionService;
    private readonly ICostCalculatorService _costCalculator;
    private readonly TaskTrackerDb _context;

    public TaskService(IWorkerSelectionService workerSelectionService, ICostCalculatorService costCalculator,
        TaskTrackerDb context)
    {
        _workerSelectionService = workerSelectionService;
        _costCalculator = costCalculator;
        _context = context;
    }

    public async Task<TaskCreationResponse> Create(TaskCreationRequest model)
    {
        var workerId = await _workerSelectionService.GetUserIdToAssign(model);
        var assignmentFee = _costCalculator.CalculateAssignmentFee(model);
        var finalizationReward = _costCalculator.CalculateFinalizationReward(model);

        var task = new Task(model.Title!, model.Description!, workerId, assignmentFee, finalizationReward);

        _context.Tasks.Add(task);
        await _context.SaveChangesAsync();

        return new TaskCreationResponse(task);
    }
}

internal interface ICostCalculatorService
{
    int CalculateAssignmentFee(TaskCreationRequest model);

    int CalculateFinalizationReward(TaskCreationRequest model);
}

internal class CostCalculatorService : ICostCalculatorService
{
    private readonly Random _random;

    internal CostCalculatorService()
    {
        _random = new Random();
    }

    public int CalculateAssignmentFee(TaskCreationRequest model) => _random.Next(-10, -20);

    public int CalculateFinalizationReward(TaskCreationRequest model) => _random.Next(20, 40);
}

internal interface IWorkerSelectionService
{
    Task<Guid> GetUserIdToAssign(TaskCreationRequest model);
}

internal class WorkerSelectionService : IWorkerSelectionService
{
    private readonly TaskTrackerDb _context;

    private readonly Random _random;

    internal WorkerSelectionService(TaskTrackerDb context)
    {
        _context = context;
        _random = new Random();
    }

    public async Task<Guid> GetUserIdToAssign(TaskCreationRequest model)
    {
        var usersCount = await _context.Users.CountAsync();

        var randomUser = _random.Next(0, usersCount);

        return _context.Users.ElementAt(randomUser).Id;
    }
}