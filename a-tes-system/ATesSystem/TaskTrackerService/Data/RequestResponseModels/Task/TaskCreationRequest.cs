using System.ComponentModel.DataAnnotations;

namespace TaskTrackerService.Data.RequestResponseModels.Task;

public class TaskCreationRequest
{
    [Required]
    public string? Title { get; set; }

    [Required]
    public string? Description { get; set; }
}