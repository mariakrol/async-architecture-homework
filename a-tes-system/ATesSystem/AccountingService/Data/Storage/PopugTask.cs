using System.ComponentModel.DataAnnotations.Schema;

namespace AccountingService.Data.Storage;

[Table("tasks")]
public class PopugTask
{
    [System.ComponentModel.DataAnnotations.Key]
    [Column("id")]
    public Guid Id { get; set; }

    public PopugTask(Guid id, Guid userId, DateTime creationDate, string description, int assigmentFee, int finalizationReward)
    {
        Id = id;
        UserId = userId;
        CreationDate = creationDate;
        Description = description;
        AssigmentFee = assigmentFee;
        FinalizationReward = finalizationReward;
    }

    public PopugTask() {}

    [Column("assigned-user-id")]
    public Guid? UserId { get; set; }

    [Column("creation-date")]
    public DateTime CreationDate { get; set; }

    [Column("description")]
    public string Description { get; set; }

    [Column("assigment-fee")]
    public int AssigmentFee { get; set; }

    [Column("reward")]
    public int FinalizationReward { get; set; }

    internal void UnassignUser() => UserId = null;

    internal void AssignUser(Guid userId) => UserId = userId;
}