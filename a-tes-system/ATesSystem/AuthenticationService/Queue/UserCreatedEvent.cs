using AuthenticationService.Data.RequestResponseModels.User;
using PopugKafkaClient.Producer;

namespace AuthenticationService.Queue;

public class UserCreatedEvent : MessageQueueEventBase<UserCreationResponse>
{
    public UserCreatedEvent(UserCreationResponse payload)
    {
        Payload = payload;
    }

    public override string EventName => "user-created";
    public override UserCreationResponse Payload { get; }
}