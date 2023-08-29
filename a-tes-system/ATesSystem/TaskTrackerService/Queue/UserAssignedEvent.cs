using PopugKafkaClient.Producer;

namespace AuthenticationService.Queue;

public class UserAssignedEvent : MessageQueueEventBase<AssigmentChangeEventPayload>
{
    public UserAssignedEvent(AssigmentChangeEventPayload payload)
    {
        Payload = payload;
    }

    public override string EventName => "user-assigned";
    
    public override AssigmentChangeEventPayload Payload { get; }
}
