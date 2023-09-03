using System.ComponentModel.DataAnnotations.Schema;

namespace TaskTrackerService.Data.Storage;

[Table("tasks")]
public class PopugTask
{
    [System.ComponentModel.DataAnnotations.Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("assigned-user-id")]
    public Guid AssignedUserId { get; set; }

    [Column("title")]
    public string Title { get; set; }

    [Column("description")]
    public string Description { get; set; }

    [Column("creation-date")]
    public DateTime CreatedDate { get; set; }

    [Column("status")]
    public TaskStatus Status { get; set; }

    [Column("assigment-fee")]
    public int AssignmentFee { get; set; }

   [Column("reward")]
    public int FinalizationReward { get; set; }

    public void ReassignUser(Guid userId) => AssignedUserId = userId;

    public void ChangeStatus(TaskStatus status) => Status = status;
}