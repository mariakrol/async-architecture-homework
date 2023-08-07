using System.Text.Json.Serialization;

namespace AuthenticationService.Data.Storage;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Role
{
    Admin,
    Worker
}
