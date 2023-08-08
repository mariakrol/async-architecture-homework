namespace AuthenticationService.Queue.Event.User;

public class UserCreatedEvent : MessageQueueEventBase<UserCreatedEventData>
{
    public UserCreatedEvent(UserCreatedEventData payload)
    {
        Payload = payload;
    }

    public override string EventName => "user-created";
    public override UserCreatedEventData Payload { get; }
}