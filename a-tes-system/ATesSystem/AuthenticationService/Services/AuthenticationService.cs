using System.Security.Authentication;
using AuthenticationService.Data.RequestResponseModels.Authentication;
using AuthenticationService.Data.Storage;

namespace AuthenticationService.Services;

internal class AuthenticationService : IAuthenticationService
{
    private readonly IUserService _userService;

    public AuthenticationService(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<AuthenticationResponse> Authenticate(AuthenticationRequest model)
    {
        User? user;

        try
        {
            user = await TryGetUser(model);
        }
        catch (ArgumentException e)
        {
            throw new AuthenticationException($"Authentication failed for the user '{model.UserName}'", e);
        }

        if (user is null)
        {
            throw new AuthenticationException($"User is not found by authentication data. Username: '{model.UserName}'");
        }

        //ToDo: generate token

        throw new NotImplementedException();
    }

    private async Task<User> TryGetUser(AuthenticationRequest model)
    {
        if (model.UserName is null)
        {
            throw new ArgumentNullException(nameof(model.UserName));
        }
        if (model.Password is null)
        {
            throw new ArgumentNullException(nameof(model.Password));
        }

        return await _userService.RetrieveUser(model.UserName, model.Password);
    }
}