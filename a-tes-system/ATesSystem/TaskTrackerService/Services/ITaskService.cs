using TaskTrackerService.Data.RequestResponseModels.Task;
using TaskTrackerService.Data.Storage;

namespace TaskTrackerService.Services;

public interface ITaskService
{
    Task<PopugTask[]> GetTasks();

    Task<PopugTask[]> GetAssignedTasks();

    Task<TaskCreationResponse> Create(TaskCreationRequest model);

    Task ShuffleTasks();

    Task FinishTask(Guid taskId);
}