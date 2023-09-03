namespace AccountingService.Queue;

public class UserAssignedEvent : AssigmentChangeEvent
{
    public UserAssignedEvent(Guid taskId, Guid userId) : base(taskId)
    {
        UserId = userId;
    }

    public Guid UserId { get; set; }
}