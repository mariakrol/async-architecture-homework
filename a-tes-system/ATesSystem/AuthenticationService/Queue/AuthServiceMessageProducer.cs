using Microsoft.Extensions.Options;
using PopugKafkaClient.Data.Configuration;
using PopugKafkaClient.Producer;

namespace AuthenticationService.Queue;

public class AuthServiceMessageProducer : MessageQueueEventProducerService
{
    public AuthServiceMessageProducer(IOptions<PopugKafkaSettings> settings)
        : base(settings, "auth-service")
    {
    }
}