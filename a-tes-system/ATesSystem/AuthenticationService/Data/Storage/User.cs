namespace AuthenticationService.Data.Storage;

public class User
{
    public User(Guid id, string name, string encryptedPassword, Role[] roles)
    {
        Id = id;
        Name = name;
        EncryptedPassword = encryptedPassword;
        Roles = roles;
    }

    public Guid Id { get; set; }

    public string Name { get; set; }

    public string EncryptedPassword { get; set; }

    public Role[] Roles { get; set; }
}
