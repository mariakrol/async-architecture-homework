using TaskTrackerService.Data.RequestResponseModels.Task;

namespace TaskTrackerService.Services;

public interface ICostCalculatorService
{
    int CalculateAssignmentFee(TaskCreationRequest model);

    int CalculateFinalizationReward(TaskCreationRequest model);
}