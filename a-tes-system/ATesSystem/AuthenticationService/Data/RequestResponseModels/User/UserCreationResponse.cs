using AuthenticationService.Data.Storage;

namespace AuthenticationService.Data.RequestResponseModels.User;

public class UserCreationResponse
{
    public UserCreationResponse(Storage.User user)
    {
        Id = user.Id;
        Name = user.Name;
        Role = user.Role;
    }

    public Guid Id { get; }

    public string Name { get; }

    public Role Role { get; }
}
