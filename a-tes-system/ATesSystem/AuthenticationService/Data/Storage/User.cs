namespace AuthenticationService.Data.Storage;

public class User
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public string? EncryptedPassword { get; set; }

    public Role[]? Roles { get; set; }
}
