using Microsoft.AspNetCore.Mvc;
using TaskTrackerService.Attributes;
using TaskTrackerService.Data.Storage;
using TaskTrackerService.Services;

namespace TaskTrackerService.Controller;

/// <summary>
/// The controller is created just for simplifying of check that consuming of
/// Kafka event works properly
/// </summary>
[ApiController]
[Route("[Controller]")]
public class UsersController : Microsoft.AspNetCore.Mvc.Controller
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    [Produces("application/json")]
    [AllowAnonymous]
    public async Task<User[]> GetUsers()
    {
        return await _userService.GetUsers();
    }
}