public class Account
{
    public Account(Guid id, Guid userId)
    {
        Id = id;
        UserId = userId;
    }

    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public int Income { get; set; }

    public void ChangeBalance(int fee)
    {
        Income += fee;
    }
}