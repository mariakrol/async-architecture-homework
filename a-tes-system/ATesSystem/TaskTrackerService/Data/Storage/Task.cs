using System.Collections.Generic;

namespace TaskTrackerService.Data.Storage;

public class Task
{
    public Task(string title, string description)
    {
        Id = Guid.NewGuid();
        Title = title;
        Description = description;
        CreatedDate = DateTime.Now;
        Status = TaskStatus.New;
    }

    public Guid Id { get; }

    public Guid AssignedUserId { get; private set; }

    public string Title { get; }

    public string Description { get; }

    public DateTime CreatedDate { get; }

    public TaskStatus Status { get; private set; }

    public int AssignmentFee { get; private set; }

    public int FinalizationReward { get; private set; }

    public void AssignUser(Guid userId) => AssignedUserId = userId;

    public void AddAssignmentFee(int fee) => AssignmentFee = fee;

    public void AddFinalizationReward(int reward) => FinalizationReward = reward;
}