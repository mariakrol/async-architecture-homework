using System.Text.Json;

namespace PopugKafkaClient.Producer;

public abstract class MessageQueueEventBase<TPayload>
{
    public abstract string EventName { get; }

    public abstract TPayload Payload { get; }
}