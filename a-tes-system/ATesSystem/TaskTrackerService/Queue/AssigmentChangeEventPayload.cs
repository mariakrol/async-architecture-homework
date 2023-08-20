namespace AuthenticationService.Queue;

public class AssigmentChangeEventPayload
{
    public AssigmentChangeEventPayload(Guid taskId, Guid userId)
    {
        TaskId = taskId;
        UserId = userId;
    }

    public Guid TaskId { get; set; }

    public Guid UserId { get; set; }
}