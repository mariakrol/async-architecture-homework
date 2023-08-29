public class UserAssignedEvent
{
    public UserAssignedEvent(Guid taskId, Guid userId)
    {
        TaskId = taskId;
        UserId = userId;
    }

    public Guid TaskId { get; set; }

    public Guid UserId { get; set; }
}