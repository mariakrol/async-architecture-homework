using System.Text.Json;
using Confluent.Kafka;

namespace PopugKafkaClient.Utilities;

public class PayloadSerializer<TPayload> : ISerializer<TPayload>, IDeserializer<TPayload>
{
    public byte[] Serialize(TPayload data, SerializationContext context)
    {
        using var stream = new MemoryStream();
        var jsonString = JsonSerializer.Serialize(data);
        var writer = new StreamWriter(stream);

        writer.Write(jsonString);
        writer.Flush();
        stream.Position = 0;

        return stream.ToArray();
    }

    TPayload IDeserializer<TPayload>.Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
    {
        return JsonSerializer.Deserialize<TPayload>(data.ToArray());
    }
}