﻿using AuthenticationService.Data.RequestResponseModels.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Authentication;
using AuthenticationService.Attributes;

using IAuthenticationService = AuthenticationService.Services.IAuthenticationService;

namespace AuthenticationService.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuthenticationResponse))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [HttpPost("authenticate")]
    [AllowAnonymous]
    public async Task<IActionResult> Authenticate(AuthenticationRequest model)
    {
        try
        {
            return Ok(await _authenticationService.Authenticate(model));
        }
        catch (AuthenticationException ex)
        {
            // Only message is sent to the caller, because System.Text.Json
            // does not support serialization of exceptions https://github.com/dotnet/runtime/issues/43026
            return Unauthorized(ex.Message);
        }
    }
}
