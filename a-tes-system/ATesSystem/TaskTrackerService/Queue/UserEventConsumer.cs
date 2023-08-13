using Microsoft.Extensions.Options;
using PopugKafkaClient.Consumer;
using PopugKafkaClient.Data.Configuration;
using TaskTrackerService.Data.Storage;

namespace TaskTrackerService.Queue;

public class UserEventConsumer : MessageQueueEventConsumer<UserCreatedEvent>
{
    public UserEventConsumer(TaskTrackerDb dataContext, IOptions<PopugKafkaSettings> settings)
        : base(settings, "task-tracker-user-event-consumer", "user-created", 
            eventData => SaveUserToDb(dataContext, eventData)) { }

    public static void SaveUserToDb(TaskTrackerDb dataContext, UserCreatedEvent @event)
    {
        var user = new User(@event.Id, @event.UserName, @event.Role);
        dataContext.Users.Add(user);
        dataContext.SaveChanges();
    }
}