using TaskTrackerService.Data.RequestResponseModels.Task;

namespace TaskTrackerService.Services;

internal class TaskService : ITaskService
{
    public Task<TaskCreationResponse> Create(TaskCreationRequest model)
    {
        throw new NotImplementedException();
    }
}