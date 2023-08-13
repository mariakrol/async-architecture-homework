using TaskTrackerService.Data.RequestResponseModels.Task;

namespace TaskTrackerService.Services;

public class CostCalculatorService : ICostCalculatorService
{
    private readonly Random _random = new();

    public int CalculateAssignmentFee(TaskCreationRequest model) => _random.Next(-10, -20);

    public int CalculateFinalizationReward(TaskCreationRequest model) => _random.Next(20, 40);
}