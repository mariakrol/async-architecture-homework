using AuthenticationService.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationService.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]
public class ErrorController : Controller
{
    [Route("/error")]
    [AllowAnonymous]
    public IActionResult HandleError() => Problem();

    [HttpGet("throw")]
    [AllowAnonymous]
    public IActionResult Throw() => throw new Exception("Sample exception.");
}