using AuthenticationService.Queue;
using Microsoft.EntityFrameworkCore;
using PopugKafkaClient.Producer;
using TaskTrackerService.CustomExceptions;
using TaskTrackerService.Data.RequestResponseModels.Task;
using TaskTrackerService.Data.Storage;
using TaskStatus = TaskTrackerService.Data.Storage.TaskStatus;

namespace TaskTrackerService.Services;

public class TaskService : ITaskService
{
    private readonly IWorkerSelectionService _workerSelectionService;
    private readonly ICostCalculatorService _costCalculator;
    private readonly TaskTrackerDb _dataContext;
    private readonly IHttpContextAccessor _httpContext;
    private readonly IMessageQueueEventProducerService _queueEventProducer;

    public TaskService(IWorkerSelectionService workerSelectionService,
        ICostCalculatorService costCalculator,
        IMessageQueueEventProducerService queueEventProducer,
        TaskTrackerDb context,
        IHttpContextAccessor httpContext)
    {
        _workerSelectionService = workerSelectionService;
        _costCalculator = costCalculator;
        _queueEventProducer = queueEventProducer;
        _dataContext = context;
        _httpContext = httpContext;
    }
    
    public async Task<TaskCreationResponse> Create(TaskCreationRequest model)
    {
        var foo = await _dataContext.Tasks.CountAsync();
        Console.WriteLine($"Before creation: {foo}");

        var id = new Guid();

        var workerId = await _workerSelectionService.GetUserIdToAssign(new WorkerAssignRequest(model, id));
        var assignmentFee = _costCalculator.CalculateAssignmentFee(model, id);
        var finalizationReward = _costCalculator.CalculateFinalizationReward(model, id);

        var task = new PopugTask() {
            Title = model.Title!,
            Description = model.Description!, 
            AssignedUserId = workerId, 
            AssignmentFee = assignmentFee, 
            FinalizationReward = finalizationReward,
            Id = id,
            CreatedDate = DateTime.Now,
            Status = TaskStatus.Assigned
        };

        _dataContext.Tasks.Add(task);
        await _dataContext.SaveChangesAsync();

        foo = await _dataContext.Tasks.CountAsync();
        Console.WriteLine($"After creation: {foo}");

        var response = new TaskCreationResponse(task);

        var queueEvent = new TaskCreatedEvent(response);
        await _queueEventProducer.Produce("tasks", queueEvent);
        await _queueEventProducer.Produce("tasks-stream", queueEvent);

        return response;
    }

    public async Task<PopugTask[]> GetTasks()
    {
        var foo = await _dataContext.Tasks.CountAsync();
        Console.WriteLine($"Before get: {foo}");

        return await _dataContext.Tasks.ToArrayAsync();
    }

    public async Task<PopugTask[]> GetAssignedTasks()
    {
        var authorizedUserId = GetAuthorizedUserId();

        return await _dataContext.Tasks.Where(t => t.AssignedUserId == authorizedUserId).ToArrayAsync();
    }

    public async Task ShuffleTasks()
    {
        foreach (var task in _dataContext.Tasks) {
            var taskId = task.Id;
            var initialAssignee = task.AssignedUserId;

            var workerId = await _workerSelectionService.GetUserIdToAssign(new WorkerAssignRequest(task));

            task.ReassignUser(workerId);
            await _dataContext.SaveChangesAsync();

            var afterChange = await _dataContext.Tasks.FirstAsync(t => t.Id == taskId);

            Console.WriteLine($"Initial assignee: {initialAssignee}, choosen: {workerId}, in DB: {afterChange.AssignedUserId}");
        }
    }

    public async Task FinishTask(Guid taskId)
    {
        var task = await _dataContext.Tasks.FirstAsync(t => t.Id == taskId);
        var authorizedUserId = GetAuthorizedUserId();

        if(task.AssignedUserId != authorizedUserId)
        {
            throw new TaskTrackerServiceException($"Task {taskId} is not assigned on authorized user");
        }

        task.ChangeStatus(TaskStatus.Done);
        await _dataContext.SaveChangesAsync();

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