namespace TaskTrackerService.Data.Storage;

public class PopugTask
{
    public Guid Id { get; set; }

    public Guid AssignedUserId { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public DateTime CreatedDate { get; set; }

    public TaskStatus Status { get; set; }

    public int AssignmentFee { get; set; }

    public int FinalizationReward { get; set; }

    public void ReassignUser(Guid userId) => AssignedUserId = userId;

    public void ChangeStatus(TaskStatus status) => Status = status;
}