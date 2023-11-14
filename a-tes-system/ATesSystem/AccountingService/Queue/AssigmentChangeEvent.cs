namespace AccountingService.Queue;

public class AssigmentChangeEvent
{
    public AssigmentChangeEvent(Guid taskId)
    {
        TaskId = taskId;
    }

    public Guid TaskId { get; set; }
}
