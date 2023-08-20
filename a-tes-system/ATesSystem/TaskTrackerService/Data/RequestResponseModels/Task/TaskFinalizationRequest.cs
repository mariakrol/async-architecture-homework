using System.ComponentModel.DataAnnotations;

namespace TaskTrackerService.Data.RequestResponseModels.Task;

public class TaskFinalizationRequest
{
    [Required]
    public Guid? TaskId { get; set; }
}