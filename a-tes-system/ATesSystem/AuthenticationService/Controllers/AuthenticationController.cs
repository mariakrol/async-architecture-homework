using AuthenticationService.Data.RequestResponseModels.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationService.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthenticationController : ControllerBase
{
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuthenticationResponse))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [HttpPost("authenticate")]
    public async Task<IActionResult> Authenticate(AuthenticationRequest model)
    {
        throw new NotImplementedException();
    }
}
