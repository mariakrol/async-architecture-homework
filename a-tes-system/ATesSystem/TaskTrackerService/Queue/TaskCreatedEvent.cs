using PopugKafkaClient.Producer;
using TaskTrackerService.Data.RequestResponseModels.Task;

namespace AuthenticationService.Queue;

public class TaskCreatedEvent : MessageQueueEventBase<TaskCreationResponse>
{
    public TaskCreatedEvent(TaskCreationResponse payload)
    {
        Payload = payload;
    }

    public override string EventName => "task-created";
    public override TaskCreationResponse Payload { get; }
}
