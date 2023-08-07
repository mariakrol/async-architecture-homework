using Microsoft.AspNetCore.Mvc;

namespace AuthenticationService.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]
public class ErrorController : Controller
{
    [Route("/error")]
    public IActionResult HandleError() => Problem();

    [HttpGet("throw")]
    public IActionResult Throw() => throw new Exception("Sample exception.");
}