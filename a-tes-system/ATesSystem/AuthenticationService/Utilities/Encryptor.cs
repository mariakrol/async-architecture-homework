using System.Security.Cryptography;

namespace AuthenticationService.Utilities;

internal static class Encryptor
{
    internal static string Encrypt(string rawData, string? secret)
    {
        secret ??= "";
        var encoding = new System.Text.ASCIIEncoding();
        var keyByte = encoding.GetBytes(secret);
        var rawBytes = encoding.GetBytes(rawData);

        using var hmac256 = new HMACSHA256(keyByte);
        var hashMessage = hmac256.ComputeHash(rawBytes);
        return Convert.ToBase64String(hashMessage);
    }
}