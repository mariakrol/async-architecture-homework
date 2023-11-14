using AuthenticationService.Attributes;
using AuthenticationService.Data.RequestResponseModels.User;
using AuthenticationService.Data.Storage;
using AuthenticationService.Services;
using Microsoft.AspNetCore.Mvc;
using PopugKafkaClient.Producer;

namespace AuthenticationService.Controllers;

[ApiController]
[Route("[Controller]")]
//[Authorize]
public class UsersController : Controller
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService, IMessageQueueEventProducerService queueEventProducer)
    {
        _userService = userService;
    }

    [HttpPost]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UserCreationResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IResult> CreateAccount(UserCreationRequest model)
    {
        var response = await _userService.CreateUser(model);

        return TypedResults.Created($"/users/{response.Id}", response);
    }

    /// <summary>
    /// The request is created just to simplify test and debug process.
    /// Will be removed in the final realization
    /// </summary>
    /// <returns>set of existing users</returns>
    [HttpGet]
    [Produces("application/json")]
    [AllowAnonymous]
    public async Task<User[]> GetUsers()
    {
        return await _userService.GetUsers();
    }
}