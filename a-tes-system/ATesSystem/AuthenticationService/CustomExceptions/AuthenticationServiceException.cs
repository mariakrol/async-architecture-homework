namespace AuthenticationService.CustomExceptions;

public class AuthenticationServiceException : Exception
{
    public AuthenticationServiceException() { }

    public AuthenticationServiceException(string message) : base(message) { }

    public AuthenticationServiceException(string message,  Exception innerException) : base(message, innerException) { }

    public AuthenticationServiceException(Exception innerException)
        : base("Internal error in AuthenticationService", innerException) { }
}