using Microsoft.AspNetCore.Mvc;
using TaskTrackerService.Data.Storage;
using TaskTrackerService.Services;

namespace TaskTrackerService.Controller;

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
    public async Task<User[]> GetUsers()
    {
        return await _userService.GetUsers();
    }
}