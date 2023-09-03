using System.ComponentModel.DataAnnotations.Schema;

namespace AccountingService.Data.Storage;

[Table("users")]
public class User
{
    public User(Guid id, string name, Role role)
    {
        Name = name;
        Id = id;
        Role = role;
    }

    public User() {}
    
    [System.ComponentModel.DataAnnotations.Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("name")]
    public string Name { get; set; }

    [Column("role")]
    public Role Role { get; set; }
}