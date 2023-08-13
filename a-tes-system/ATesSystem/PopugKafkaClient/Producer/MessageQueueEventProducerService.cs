using Confluent.Kafka;
using PopugKafkaClient.Data.Configuration;
using PopugKafkaClient.Utilities;
using Microsoft.Extensions.Options;

namespace PopugKafkaClient.Producer;

public class MessageQueueEventProducerService : IMessageQueueEventProducerService
{
    private readonly ProducerConfig _config;

    public MessageQueueEventProducerService(IOptions<PopugKafkaSettings> kafkaSettings, string clientId)
    {
        _config = new()
        {
            BootstrapServers = kafkaSettings.Value.ServerAddress,
            ClientId = clientId,
            BrokerAddressFamily = kafkaSettings.Value.AddressFamily
        };
    }

    public Task Produce<TPayload>(string topic, MessageQueueEventBase<TPayload> @event)
    {
        IProducer<string, TPayload> producer = null;

        try
        {
            producer = new ProducerBuilder<string, TPayload>(_config)
                .SetValueSerializer(new PayloadSerializer<TPayload>())
                .Build();

            producer.Produce(topic,
                new Message<string, TPayload> { Key = @event.EventName, Value = @event.Payload },
                deliveryReport =>
                {
                    Console.WriteLine(deliveryReport.Error.Code != ErrorCode.NoError
                        ? $"Failed to deliver message: {deliveryReport.Error.Reason}"
                        : $"Produced message to: {deliveryReport.TopicPartitionOffset}");
                });

            return Task.CompletedTask;

        }
        catch (Exception e)
        {
            return Task.FromException(e);
        }
        finally
        {
            var queueSize = producer?.Flush(TimeSpan.FromSeconds(5));
            if (queueSize > 0)
            {
                Console.WriteLine("WARNING: Producer event queue has " + queueSize + " pending events on exit.");
            }

            producer?.Dispose();
        }
    }
}