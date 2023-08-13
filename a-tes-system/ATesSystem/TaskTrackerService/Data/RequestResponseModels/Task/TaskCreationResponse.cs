namespace TaskTrackerService.Data.RequestResponseModels.Task;

public class TaskCreationResponse
{
    internal TaskCreationResponse(Storage.PopugTask popugTask)
    {
        Id = popugTask.Id;
        AssignedUser = popugTask.AssignedUserId;
        AssignmentFee = popugTask.AssignmentFee;
        FinalizationReward = popugTask.FinalizationReward;
    }

    public Guid? Id { get; set; }

    public Guid? AssignedUser { get; set; }

    public int AssignmentFee { get; set; }

    public int FinalizationReward { get; set; }
}
