using PopugKafkaClient.Producer;

namespace AuthenticationService.Queue;

public class UserUnassignedEvent : MessageQueueEventBase<AssigmentChangeEventPayload>
{
    public UserUnassignedEvent(AssigmentChangeEventPayload payload)
    {
        Payload = payload;
    }

    public override string EventName => "user-unassigned";
    public override AssigmentChangeEventPayload Payload { get; }
}
