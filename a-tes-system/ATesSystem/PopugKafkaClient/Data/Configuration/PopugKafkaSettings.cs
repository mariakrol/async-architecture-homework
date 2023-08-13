using Confluent.Kafka;

namespace PopugKafkaClient.Data.Configuration;

public class PopugKafkaSettings
{
    public string? ServerAddress { get; set; }

    public BrokerAddressFamily AddressFamily { get; set; }
}