namespace TaskTrackerService.Utilities.Jwt;

public interface IJwtUtils
{
    public Guid? ValidateJwtToken(string? token);
}