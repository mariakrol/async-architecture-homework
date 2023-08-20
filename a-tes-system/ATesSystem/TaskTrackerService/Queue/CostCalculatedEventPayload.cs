namespace AuthenticationService.Queue;

public class CostCalculatedEventPayload
{
    public CostCalculatedEventPayload(Guid taskId, int fee)
    {
        TaskId = taskId;
        Fee = fee;
    }

    public Guid TaskId { get; set; }

    public int Fee { get; set; }
}