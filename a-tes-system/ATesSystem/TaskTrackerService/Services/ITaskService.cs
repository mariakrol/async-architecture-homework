using TaskTrackerService.Data.RequestResponseModels.Task;

namespace TaskTrackerService.Services;

public interface ITaskService
{
    Task<TaskCreationResponse> Create(TaskCreationRequest model);
}