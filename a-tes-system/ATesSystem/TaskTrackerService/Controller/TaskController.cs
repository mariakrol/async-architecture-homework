using Microsoft.AspNetCore.Mvc;
using TaskTrackerService.Attributes;
using TaskTrackerService.Data.RequestResponseModels.Task;
using TaskTrackerService.Data.Storage;
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

    [HttpGet("all")]
    [Produces("application/json")]
    [AuthorizeAsAdmin]
    public async Task<PopugTask[]> GetTasks()
    {
        return await _taskService.GetTasks();
    }

    [HttpGet("assigned")]
    [Produces("application/json")]
    [Authorize]
    public async Task<PopugTask[]> GetAssignedTasks()
    {
        return await _taskService.GetAssignedTasks();
    }

    [HttpPost("create")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(TaskCreationResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Authorize]
    public async Task<IResult> CreateTask(TaskCreationRequest model)
    {
        var response = await _taskService.Create(model);

        return TypedResults.Created($"/tasks/{response.Id}", response);
    }

    [HttpPost("shuffle")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [AuthorizeAsAdmin]
    public async Task ShuffleTasks()
    {
        await _taskService.ShuffleTasks();
    }

    [HttpPost("finish")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Authorize]
    public async Task FinishTask(TaskFinalizationRequest request)
    {
        await _taskService.FinishTask(request.TaskId!.Value);
    } 
}