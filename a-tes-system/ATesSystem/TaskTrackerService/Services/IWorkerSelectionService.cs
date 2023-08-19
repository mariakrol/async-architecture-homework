namespace TaskTrackerService.Services;

public interface IWorkerSelectionService
{
    Task<Guid> GetUserIdToAssign(WorkerAssignRequest request);
}