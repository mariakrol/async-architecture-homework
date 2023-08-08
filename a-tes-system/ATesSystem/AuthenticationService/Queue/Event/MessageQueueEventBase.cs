namespace AuthenticationService.Queue.Event;

public abstract class MessageQueueEventBase<TPayload>
{
    public abstract string EventName { get; }

    public abstract TPayload Payload { get; }
}