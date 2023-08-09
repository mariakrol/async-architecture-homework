namespace TaskTrackerService.Data.RequestResponseModels.Task;

public class TaskCreationResponse
{
    internal TaskCreationResponse(Storage.Task task)
    {
        Id = task.Id;
        AssignedUser = task.AssignedUserId;
        AssignmentFee = task.AssignmentFee;
        FinalizationReward = task.FinalizationReward;
    }

    public Guid? Id { get; set; }

    public Guid? AssignedUser { get; set; }

    public int AssignmentFee { get; set; }

    public int FinalizationReward { get; set; }
}
