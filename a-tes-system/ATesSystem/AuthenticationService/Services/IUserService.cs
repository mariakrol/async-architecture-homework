using AuthenticationService.Data.Storage;

namespace AuthenticationService.Services;

internal interface IUserService
{
    Task<User> CreateUser(string name, string password, Role role);

    Task<User> RetrieveUser(string username, string password);
}