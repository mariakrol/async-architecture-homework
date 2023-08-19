using TaskTrackerService.Data.RequestResponseModels.Task;
using TaskTrackerService.Data.Storage;

namespace TaskTrackerService.Services;

public interface ITaskService
{
    Task<PopugTask[]> GetTasks();

    Task<PopugTask[]> GetTasks(User user);

    Task<TaskCreationResponse> Create(TaskCreationRequest model);

    Task ShuffleTasks();

    Task Finalize(Guid taskId);
}