using System.Text;

namespace PopugKafkaClient.Utilities;

public static class StringExtensions
{
    public static byte[] ToAsciiByteArray(this string value) => Encoding.ASCII.GetBytes(value);
}