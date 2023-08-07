using AuthenticationService.Data.Storage;

namespace AuthenticationService.Utilities.Jwt;

public interface IJwtUtils
{
    public string? GenerateJwtToken(User user);
}