using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TaskTrackerService.Data.Storage;

namespace TaskTrackerService.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeAsAdminAttribute : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
        if (allowAnonymous)
            return;

        var account = (User?)context.HttpContext.Items["User"];
        Console.WriteLine($"Authorization attempt. User in session - {(account is not null ? "exists" : "not exists")}");

        if (account is not null)
        {
            Console.WriteLine($"Name: {account.Name}, Role: {account.Role}");
        }

        if (account is null || account.Role != Role.Admin)
        {
            context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
        }
    }
}