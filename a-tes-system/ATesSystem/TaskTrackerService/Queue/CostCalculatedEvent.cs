using PopugKafkaClient.Producer;

namespace AuthenticationService.Queue;

public class CostCalculatedEvent : MessageQueueEventBase<CostCalculatedEventPayload>
{
    public CostCalculatedEvent(CostCalculatedEventPayload payload)
    {
        Payload = payload;
    }

    public override string EventName => "cost-calculated";
    
    public override CostCalculatedEventPayload Payload { get; }
}
