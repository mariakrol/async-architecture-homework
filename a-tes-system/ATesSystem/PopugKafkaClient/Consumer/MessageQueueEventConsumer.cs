using Confluent.Kafka;
using Microsoft.Extensions.Options;
using PopugKafkaClient.Data.Configuration;
using PopugKafkaClient.Utilities;

namespace PopugKafkaClient.Consumer;

public abstract class MessageQueueEventConsumer<TPayload>
{
    protected MessageQueueEventConsumer(IOptions<PopugKafkaSettings> settings, string clientId, string topic, Action<TPayload> handleMessage)
    {
        ConsumerConfig config = new()
        {
            BootstrapServers = settings.Value.ServerAddress,
            AutoOffsetReset = AutoOffsetReset.Earliest,
            ClientId = clientId,
            //GroupId = "my-group",
            BrokerAddressFamily = settings.Value.AddressFamily
        };

        IConsumer<Ignore, TPayload> consumer = new ConsumerBuilder<Ignore, TPayload>(config)
            .SetValueDeserializer(new PayloadSerializer<TPayload>())
            .Build();
        consumer.Subscribe(topic);

        consumer.Subscribe(topic);

        Task.Run(() =>
        {
            while (true)
            {
                try
                {
                    var consumeResult = consumer.Consume();

                    if (consumeResult != null)
                    {
                        handleMessage(consumeResult.Message.Value);
                        Console.WriteLine($"Received message: {consumeResult.Message.Value}");
                    }
                    else
                    {
                        Console.WriteLine("Empty message received"); //ToDo: Log as Warn?
                    }
                }
                catch (ConsumeException e)
                {
                    Console.WriteLine($"Error while consuming message: {e.Error.Reason}");
                }
            }
        });
    }
}