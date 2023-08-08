using AuthenticationService.Queue.Event;

namespace AuthenticationService.Queue;

public interface IMessageQueueEventProducerService
{
    Task Produce<TPayload>(string topic, MessageQueueEventBase<TPayload> eventDescription);
}