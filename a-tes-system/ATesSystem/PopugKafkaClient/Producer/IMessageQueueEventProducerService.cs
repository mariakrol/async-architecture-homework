namespace PopugKafkaClient.Producer;

public interface IMessageQueueEventProducerService
{
    public Task Produce<TPayload>(string topic, MessageQueueEventBase<TPayload> @event);
}