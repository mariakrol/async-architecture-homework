using Microsoft.AspNetCore.Mvc;
using TaskTrackerService.Data.RequestResponseModels.Task;
using TaskTrackerService.Services;

namespace TaskTrackerService.Controller;

[ApiController]
[Route("[Controller]")]
public class TaskController : ControllerBase
{
    private readonly ITaskService _taskService;

    public TaskController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpPost]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(TaskCreationResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IResult> CreateTodo(TaskCreationRequest model)
    {
        var response = await _taskService.Create(model);

        return TypedResults.Created($"/tasks/{response.Id}", response);
    }
}