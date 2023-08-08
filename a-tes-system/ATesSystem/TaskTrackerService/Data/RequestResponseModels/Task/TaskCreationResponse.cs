namespace TaskTrackerService.Data.RequestResponseModels.Task;

public class TaskCreationResponse
{
    public Guid? Id { get; set; }

    public Guid? AssignedUser { get; set; }

    public int AssignmentFee { get; set; }

    public int FinalizationReward { get; set; }
}
