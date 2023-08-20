using AuthenticationService.Attributes;
using AuthenticationService.Data.RequestResponseModels.User;
using AuthenticationService.Data.Storage;
using AuthenticationService.Queue;
using AuthenticationService.Services;
using Microsoft.AspNetCore.Mvc;
using PopugKafkaClient.Producer;

namespace AuthenticationService.Controllers;

[ApiController]
[Route("[Controller]")]
[Authorize]
public class UsersController : Controller
{
    private readonly IUserService _userService;
    private readonly IMessageQueueEventProducerService _queueEventProducer;

    public UsersController(IUserService userService, IMessageQueueEventProducerService queueEventProducer)
    {
        _userService = userService;
        _queueEventProducer = queueEventProducer;
    }

    [HttpPost]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UserCreationResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IResult> CreateAccount(UserCreationRequest model)
    {
        var response = await _userService.CreateUser(model);

        var userCreatedEvent = new UserCreatedEvent(response);
        await _queueEventProducer.Produce("users-stream", userCreatedEvent);

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