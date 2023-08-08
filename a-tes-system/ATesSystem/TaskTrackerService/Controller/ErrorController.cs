using Microsoft.AspNetCore.Mvc;

namespace TaskTrackerService.Controller;

[ApiExplorerSettings(IgnoreApi = true)]
public class ErrorController : Microsoft.AspNetCore.Mvc.Controller
{
    [Route("/error")]
    public IActionResult HandleError() => Problem();

    [HttpGet("throw")]
    public IActionResult Throw() => throw new Exception("Sample exception.");
}