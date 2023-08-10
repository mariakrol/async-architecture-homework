using AuthenticationService.Data.RequestResponseModels.User;
using AuthenticationService.Data.Storage;

namespace AuthenticationService.Services;

public interface IUserService
{
    Task<UserCreationResponse> CreateUser(UserCreationRequest model);

    internal Task<User> CreateUser(string name, string password, Role role);

    internal Task<User> RetrieveUser(string username, string password);
}