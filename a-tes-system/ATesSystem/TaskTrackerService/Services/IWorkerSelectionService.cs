using TaskTrackerService.Data.RequestResponseModels.Task;

namespace TaskTrackerService.Services;

public interface IWorkerSelectionService
{
    Task<Guid> GetUserIdToAssign(TaskCreationRequest model);
}