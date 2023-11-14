using System.ComponentModel.DataAnnotations.Schema;

namespace TaskTrackerService.Data.Storage;

[Table("users")]
public class User
{
    public User(Guid id, string name, Role role)
    {
        Id = id;
        Name = name;
        Role = role;
    }

    [System.ComponentModel.DataAnnotations.Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("name")]
    public string Name { get; set; }

    [Column("role")]
    public Role Role { get; set; }
}