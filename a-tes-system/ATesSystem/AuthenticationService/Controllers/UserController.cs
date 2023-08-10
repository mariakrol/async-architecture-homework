using AuthenticationService.Attributes;
using AuthenticationService.Data.RequestResponseModels.User;
using AuthenticationService.Queue;
using AuthenticationService.Queue.Event.User;
using AuthenticationService.Services;
using Microsoft.AspNetCore.Mvc;

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

        var userCreatedEventData = new UserCreatedEventData(response.Id, response.Name, response.Role);
        var userCreatedEvent = new UserCreatedEvent(userCreatedEventData);
        await _queueEventProducer.Produce("users-stream", userCreatedEvent);

        return TypedResults.Created($"/users/{response.Id}", response);
    }
}