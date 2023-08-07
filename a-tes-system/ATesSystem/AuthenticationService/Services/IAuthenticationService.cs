using AuthenticationService.Data.RequestResponseModels.Authentication;

namespace AuthenticationService.Services;

public interface IAuthenticationService
{
    Task<AuthenticationResponse> Authenticate(AuthenticationRequest model);
}