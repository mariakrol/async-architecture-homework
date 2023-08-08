using AuthenticationService.Data.RequestResponseModels.User;
using AuthenticationService.Data.Storage;

namespace AuthenticationService.Services;

public interface IUserService
{
    Task<User> CreateUser(UserCreationRequest model);

    Task<User> CreateUser(string name, string password, Role role);

    Task<User> RetrieveUser(string username, string password);
}