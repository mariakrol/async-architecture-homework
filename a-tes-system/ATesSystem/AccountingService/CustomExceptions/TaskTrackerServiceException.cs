namespace AccountingService.CustomExceptions;

public class AccountingServiceException : Exception
{
    public AccountingServiceException() { }

    public AccountingServiceException(string message) : base(message) { }

    public AccountingServiceException(string message, Exception innerException) : base(message, innerException) { }

    public AccountingServiceException(Exception innerException)
        : base("Internal error in AuthenticationService", innerException) { }
}