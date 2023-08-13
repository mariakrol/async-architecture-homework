using Microsoft.Extensions.Options;
using TaskTrackerService.Data.Configuration;
using TaskTrackerService.Data.Storage;

namespace TaskTrackerService.Middleware;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;
    private readonly AppSettings _appSettings;

    public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings)
    {
        _next = next;
        _appSettings = appSettings.Value;
    }

    public async Task Invoke(HttpContext context, TaskTrackerDb dataContext, IJwtUtils jwtUtils)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        var userId = jwtUtils.ValidateJwtToken(token);
        if (userId != null)
        {
            context.Items["User"] = await dataContext.Users.FirstOrDefaultAsync(user => user.Id.Equals(userId.Value));
        }

        await _next(context);
    }
}