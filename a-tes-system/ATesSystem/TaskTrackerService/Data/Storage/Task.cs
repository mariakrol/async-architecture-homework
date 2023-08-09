namespace TaskTrackerService.Data.Storage;

public class Task
{
    public Task(string title, string description, Guid assignedUser, int assignmentFee, int finalizationReward)
    {
        Id = Guid.NewGuid();
        Title = title;
        Description = description;
        AssignedUserId = assignedUser;
        AssignmentFee = assignmentFee;
        FinalizationReward = finalizationReward;
        CreatedDate = DateTime.Now;
        Status = TaskStatus.Assigned;
    }

    public Guid Id { get; }

    public Guid AssignedUserId { get; private set; }

    public string Title { get; }

    public string Description { get; }

    public DateTime CreatedDate { get; }

    public TaskStatus Status { get; private set; }

    public int AssignmentFee { get; }

    public int FinalizationReward { get; }

    public void ReassignUser(Guid userId) => AssignedUserId = userId;

    public void ChangeStatus(TaskStatus status) => Status = status;
}