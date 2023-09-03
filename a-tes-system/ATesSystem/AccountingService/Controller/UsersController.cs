using AccountingService.Data.Storage;
using Microsoft.AspNetCore.Mvc;

namespace AccountingService.Controller;

/// <summary>
/// The controller is created just for simplifying of check that consuming of
/// Kafka event works properly
/// </summary>
[ApiController]
[Route("[Controller]")]
public class UsersController : Microsoft.AspNetCore.Mvc.Controller
{
    private readonly IUserService _userService;

    public UsersController(IUserService accountService)
    {
        _userService = accountService;
    }

    [HttpGet]
    [Produces("application/json")]
    public async Task<User[]> GetUsers()
    {
        return await _userService.GetUsers();
    }
}