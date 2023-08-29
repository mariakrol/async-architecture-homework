using Microsoft.Extensions.Options;
using PopugKafkaClient.Data.Configuration;
using PopugKafkaClient.Producer;

namespace TaskTrackerService.Queue;

public class TaskTrackerServiceMessageProducer : MessageQueueEventProducerService
{
    public TaskTrackerServiceMessageProducer(IOptions<PopugKafkaSettings> settings)
        : base(settings, "task-tracker-service")
    {
    }
}