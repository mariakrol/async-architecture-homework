namespace AccountingService.Data.Storage;

public class AssignedTask
{
    public AssignedTask(Guid id, Guid userId, Guid taskId)
    {
        Id = id;
        UserId = userId;
        TaskId = taskId;
    }

    public Guid Id { get; set; }
    public Guid UserId { get; }
    public Guid TaskId { get; }
}