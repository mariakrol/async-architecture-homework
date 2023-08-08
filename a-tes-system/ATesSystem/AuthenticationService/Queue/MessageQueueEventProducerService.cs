using AuthenticationService.Queue.Event;

namespace AuthenticationService.Queue;

internal class MessageQueueEventProducerService : IMessageQueueEventProducerService
{
    public async Task Produce<TPayload>(string topic, MessageQueueEventBase<TPayload> eventDescription)
    {

    }
}