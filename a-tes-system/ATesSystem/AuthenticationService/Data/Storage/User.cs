using System.ComponentModel.DataAnnotations.Schema;

namespace AuthenticationService.Data.Storage;

[Table("users")]
public class User
{
    public User(Guid id, string name, string encryptedPassword, Role role)
    {
        Id = id;
        Name = name;
        EncryptedPassword = encryptedPassword;
        Role = role;
    }

    [System.ComponentModel.DataAnnotations.Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("name")]
    public string Name { get; set; }

    [Column("encrypted_password")]
    public string EncryptedPassword { get; set; }

    [Column("role")]
    public Role Role { get; set; }
}