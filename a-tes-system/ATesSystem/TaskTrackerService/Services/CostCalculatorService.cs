using AuthenticationService.Queue;
using PopugKafkaClient.Producer;
using TaskTrackerService.Data.RequestResponseModels.Task;

namespace TaskTrackerService.Services;

public class CostCalculatorService : ICostCalculatorService
{
    private readonly Random _random;

    private readonly IMessageQueueEventProducerService _queueEventProducer;

    public CostCalculatorService(IMessageQueueEventProducerService queueEventProducer)
    {
        _queueEventProducer = queueEventProducer;
        _random = new Random();
    }

    public int CalculateAssignmentFee(TaskCreationRequest model, Guid taskId)
    {
        var fee = _random.Next(-20, -10);

        _queueEventProducer.Produce("cost-calculation-stream", 
            new CostCalculatedEvent(new CostCalculatedEventPayload(taskId, fee)));

        return fee;
    }

    public int CalculateFinalizationReward(TaskCreationRequest model, Guid taskId)
    {
        var fee = _random.Next(20, 40);

        _queueEventProducer.Produce("cost-calculation-stream", 
            new CostCalculatedEvent(new CostCalculatedEventPayload(taskId, fee)));

        return fee;
    }
}