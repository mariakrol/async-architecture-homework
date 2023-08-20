using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskTrackerService.CustomExceptions;
using TaskTrackerService.Data.RequestResponseModels.Task;
using TaskTrackerService.Data.Storage;
using TaskStatus = TaskTrackerService.Data.Storage.TaskStatus;

namespace TaskTrackerService.Services;

public class TaskService : ITaskService
{
    private readonly IWorkerSelectionService _workerSelectionService;
    private readonly ICostCalculatorService _costCalculator;
    private readonly TaskTrackerDb _context;
    private readonly IHttpContextAccessor _httpContext;

    public TaskService(IWorkerSelectionService workerSelectionService,
        ICostCalculatorService costCalculator,
        TaskTrackerDb context,
        IHttpContextAccessor httpContext)
    {
        _workerSelectionService = workerSelectionService;
        _costCalculator = costCalculator;
        _context = context;
        _httpContext = httpContext;
    }
    

    // ToDo: produce event CUD Task Created
    public async Task<TaskCreationResponse> Create(TaskCreationRequest model)
    {
        var foo = await _context.Tasks.CountAsync();
        Console.WriteLine($"Before creation: {foo}");

        var workerId = await _workerSelectionService.GetUserIdToAssign(new WorkerAssignRequest(model));
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

        foo = await _context.Tasks.CountAsync();
        Console.WriteLine($"After creation: {foo}");

        return new TaskCreationResponse(task);
    }

    public async Task<PopugTask[]> GetTasks()
    {
        var foo = await _context.Tasks.CountAsync();
        Console.WriteLine($"Before get: {foo}");

        return await _context.Tasks.ToArrayAsync();
    }

    public async Task<PopugTask[]> GetAssignedTasks()
    {
        var authorizedUserId = GetAuthorizedUserId();

        return await _context.Tasks.Where(t => t.AssignedUserId == authorizedUserId).ToArrayAsync();
    }

    public async Task ShuffleTasks()
    {
        foreach (var task in _context.Tasks) {
            var taskId = task.Id;
            var initialAssignee = task.AssignedUserId;

            var workerId = await _workerSelectionService.GetUserIdToAssign(new WorkerAssignRequest(task));

            task.ReassignUser(workerId);
            await _context.SaveChangesAsync();

            var afterChange = await _context.Tasks.FirstAsync(t => t.Id == taskId);

            Console.WriteLine($"Initial assignee: {initialAssignee}, choosen: {workerId}, in DB: {afterChange.AssignedUserId}");

            //ToDo: produce event per each user reassigning
        }
    }

    public async Task FinishTask(Guid taskId)
    {
        var task = await _context.Tasks.FirstAsync(t => t.Id == taskId);
        var authorizedUserId = GetAuthorizedUserId();

        if(task.AssignedUserId != authorizedUserId)
        {
            throw new TaskTrackerServiceException($"Task {taskId} is not assigned on authorized user");
        }

        task.ChangeStatus(TaskStatus.Done);
        await _context.SaveChangesAsync();

        // ToDo: produce events
        // Business: Task finished
        // CUD: Task updated
        // Business: fee assigned
    }


    private Guid? GetAuthorizedUserId() {
        var account = (User)_httpContext.HttpContext!.Items["User"]!; // ToDo: create 'session helper'?

        return account?.Id;
    }
}