using TaskTrackerService.Data.RequestResponseModels.Task;
using TaskTrackerService.Data.Storage;

namespace TaskTrackerService.Services;

/// <summary>
/// According the requirements, the operation of assignee search is an intellectual process. 
/// So, maybe it will collect an additional information regarding the entry task to map with a user
/// </summary>
public class WorkerAssignRequest
{
    public WorkerAssignRequest(TaskCreationRequest model, Guid id)
    {
        TaskId = id;
        Title = model.Title!;
        Description = model.Description!;
        PreviousAssignee = null;
    }

    public WorkerAssignRequest(PopugTask task)
    {
        TaskId = task.Id!;
        Title = task.Title!;
        Description = task.Description!;
        PreviousAssignee = task.AssignedUserId!;
    }

    public Guid TaskId { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public Guid? PreviousAssignee { get; set; }
}