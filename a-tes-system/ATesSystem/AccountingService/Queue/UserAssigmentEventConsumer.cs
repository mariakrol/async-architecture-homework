using Confluent.Kafka;
using Microsoft.Extensions.Options;
using PopugKafkaClient.Consumer;
using PopugKafkaClient.Data.Configuration;
using PopugKafkaClient.Utilities;

namespace AccountingService.Queue;

public class UserAssigmentEventConsumer : MessageQueueEventConsumer<UserAssignedEvent>
{
    private IDeserializer<UserAssignedEvent> _deserializer;

    public UserAssigmentEventConsumer(IServiceProvider services)
        : base(GetKafkaSettings(services),
            "accounting-user-event-consumer",
            "group2", //ToDo: read about group naming and rename
            "user-assigning-stream",
            "user-assigned")
    {
        Services = services;
        _deserializer = new PayloadSerializer<UserAssignedEvent>();
    }

    public IServiceProvider Services { get; }

    private static IOptions<PopugKafkaSettings> GetKafkaSettings(IServiceProvider services)
    {
        using var scope = services.CreateScope();
        return scope.ServiceProvider.GetService<IOptions<PopugKafkaSettings>>()!;
    }

    protected override UserAssignedEvent Deserialize(string json)
    {
        return _deserializer.Deserialize(json.ToAsciiByteArray(), isNull: false, new SerializationContext());
    }

    protected override async Task HandleMessage(UserAssignedEvent payload)
    {
        using var scope = Services.CreateScope();

        var userService = scope.ServiceProvider.GetRequiredService<IUserService>();
        await userService.AssignUser(payload.TaskId, payload.UserId);
    }
}