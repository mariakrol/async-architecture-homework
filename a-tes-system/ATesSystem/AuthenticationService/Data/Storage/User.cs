namespace AuthenticationService.Data.Storage;

public class User
{
    public User(Guid id, string name, string encryptedPassword, Role role)
    {
        Id = id;
        Name = name;
        EncryptedPassword = encryptedPassword;
        Role = role;
    }

    public Guid Id { get; set; }

    public string Name { get; set; }

    public string EncryptedPassword { get; set; }

    public Role Role { get; set; }
}
