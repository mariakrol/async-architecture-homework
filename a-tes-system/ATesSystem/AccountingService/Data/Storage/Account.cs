using System.ComponentModel.DataAnnotations.Schema;

public class Account
{
    public Account(Guid id, Guid userId)
    {
        Id = id;
        UserId = userId;
    }

    [System.ComponentModel.DataAnnotations.Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("user-id")]
    public Guid UserId { get; set; }

    [Column("balance")]
    public int Balance { get; set; }

    public void ChangeBalance(int fee)
    {
        Balance += fee;
    }
}