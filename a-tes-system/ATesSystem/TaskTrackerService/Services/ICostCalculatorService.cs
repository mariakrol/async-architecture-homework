using TaskTrackerService.Data.RequestResponseModels.Task;

namespace TaskTrackerService.Services;

public interface ICostCalculatorService
{
    int CalculateAssignmentFee(TaskCreationRequest model, Guid taskId);

    int CalculateFinalizationReward(TaskCreationRequest model, Guid taskId);
}