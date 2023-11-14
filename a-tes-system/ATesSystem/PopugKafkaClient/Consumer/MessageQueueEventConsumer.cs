﻿using Confluent.Kafka;
using Microsoft.Extensions.Options;
using PopugKafkaClient.Data.Configuration;
using Microsoft.Extensions.Hosting;

namespace PopugKafkaClient.Consumer;

public abstract class MessageQueueEventConsumer<TPayload> : BackgroundService
{
    private readonly IConsumer<string, string> _consumer;
    private readonly string _eventName;

    protected MessageQueueEventConsumer(IOptions<PopugKafkaSettings> settings, 
    string clientId, string groupId, string topic, string eventName)
    {
        var config = new ConsumerConfig()
        {
            AllowAutoCreateTopics = true,
            BootstrapServers = settings.Value.ServerAddress,
            AutoOffsetReset = AutoOffsetReset.Earliest,
            ClientId = clientId,
            GroupId = groupId
        };

        _consumer = new ConsumerBuilder<string, string>(config).Build();
        _consumer.Subscribe(topic);
        _eventName = eventName;
    }

    protected abstract Task HandleMessage(TPayload payload);

    protected abstract TPayload Deserialize(string json);

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.Yield();

        var i = 0;
        while (!stoppingToken.IsCancellationRequested)
        {
            ConsumeResult<string, string>? consumeResult;
            try
            {
                consumeResult = _consumer.Consume(stoppingToken);
            }
            catch(ConsumeException e) when (e.Message.Contains("Unknown topic or partition"))
            {
                //Log no topic found
                consumeResult = null;
            }

            if (consumeResult is not null && consumeResult.Message.Key.Equals(_eventName))
            {
                if (consumeResult.Message.Value is not null)
                {
                    Console.WriteLine($"Received message: {consumeResult.Message.Value}"); //ToDo: Log

                    var payload = Deserialize(consumeResult.Message.Value);
                    await HandleMessage(payload);
                }
                else
                {
                    Console.WriteLine("Empty message received"); //ToDo: Log
                }
                
                if (i++ % 1000 == 0)
                {
                    _consumer.Commit();
                }
            }
        }
    }

    public override async Task StopAsync(CancellationToken stoppingToken)
    {
        Console.WriteLine("Consume Scoped Service Hosted Service is stopping.");

        await base.StopAsync(stoppingToken);
    }

    public override void Dispose()
    {
        _consumer.Dispose();
        base.Dispose();
    }
}
