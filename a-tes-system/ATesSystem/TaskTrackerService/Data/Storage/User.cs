namespace TaskTrackerService.Data.Storage;

public class User
{
    public User(Guid id, string name, Role role)
    {
        Id = id;
        Name = name;
        Role = role;
    }

    public Guid Id { get; set; }

    public string Name { get; set; }

    public Role Role { get; set; }
}