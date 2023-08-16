using TaskTrackerService.Data.Storage;

namespace TaskTrackerService.Queue;

public class UserCreatedEvent
{

    public Guid Id { get; set; }

    public string Name { get; set; }

    public Role Role { get; set; }
}