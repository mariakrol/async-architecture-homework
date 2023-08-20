using TaskTrackerService.Data.RequestResponseModels.Task;
using TaskTrackerService.Data.Storage;
using TaskStatus = TaskTrackerService.Data.Storage.TaskStatus;

namespace TaskTrackerService.Services;

public class TaskService : ITaskService
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

        var task = new PopugTask() {
            Title = model.Title!,
            Description = model.Description!, 
            AssignedUserId = workerId, 
            AssignmentFee = assignmentFee, 
            FinalizationReward = finalizationReward,
            Id = new Guid(),
            CreatedDate = DateTime.Now,
            Status = TaskStatus.Assigned
        };

        _context.Tasks.Add(task);
        await _context.SaveChangesAsync();

        return new TaskCreationResponse(task);
    }
}