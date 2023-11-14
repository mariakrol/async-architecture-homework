using PopugKafkaClient.Producer;

namespace AuthenticationService.Queue;

public class UserAssignedEvent : MessageQueueEventBase<UserAssignedEventPayload>
{
    public UserAssignedEvent(UserAssignedEventPayload payload)
    {
        Payload = payload;
    }

    public override string EventName => "user-assigned";
    
    public override UserAssignedEventPayload Payload { get; }
}
