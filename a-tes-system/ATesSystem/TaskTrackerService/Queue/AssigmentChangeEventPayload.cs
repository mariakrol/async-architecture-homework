namespace AuthenticationService.Queue;

public class AssigmentChangeEventPayload
{
    public AssigmentChangeEventPayload(Guid taskId)
    {
        TaskId = taskId;
    }

    public Guid TaskId { get; set; }
}

public class UserAssignedEventPayload : AssigmentChangeEventPayload
{
    public UserAssignedEventPayload(Guid taskId, Guid userId) : base(taskId)
    {
        UserId = userId;
    }

    public Guid UserId { get; set; }
}