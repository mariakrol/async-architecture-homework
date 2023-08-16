using Confluent.Kafka;
using Microsoft.Extensions.Options;
using PopugKafkaClient.Consumer;
using PopugKafkaClient.Data.Configuration;
using PopugKafkaClient.Utilities;
using TaskTrackerService.Services;

namespace TaskTrackerService.Queue;

public class UserEventConsumer : MessageQueueEventConsumer<UserCreatedEvent>
{
    private IDeserializer<UserCreatedEvent> _deserializer;

    public UserEventConsumer(IServiceProvider services)
        : base(GetKafkaSettings(services), 
            "task-tracker-user-event-consumer",
            "group1", //ToDo: read about group naming and rename
            "users-stream")
    {
        Services = services;
        _deserializer = new PayloadSerializer<UserCreatedEvent>();
    }

    public IServiceProvider Services { get; }

    private static IOptions<PopugKafkaSettings> GetKafkaSettings(IServiceProvider services)
    {
        using var scope = services.CreateScope();
        return  scope.ServiceProvider.GetService<IOptions<PopugKafkaSettings>>()!;
    }

    protected override UserCreatedEvent Deserialize(string json)
    {
        return _deserializer.Deserialize(json.ToAsciiByteArray(), isNull: false, new SerializationContext());   
    }

    protected override async Task HandleMessage(UserCreatedEvent payload)
    {
        using var scope = Services.CreateScope();
        var userService = scope.ServiceProvider.GetRequiredService<IUserService>();

        await userService.SaveUser(payload);
    }
}