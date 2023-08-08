using AuthenticationService.Data.Storage;

namespace AuthenticationService.Queue.Event.User;

public class UserCreatedEventData
{
    public UserCreatedEventData(Guid id, string userName, Role role)
    {
        Id = id;
        UserName = userName;
        Role = role;
    }

    public Guid Id { get; }

    public string UserName { get; }

    public Role Role { get; }
}