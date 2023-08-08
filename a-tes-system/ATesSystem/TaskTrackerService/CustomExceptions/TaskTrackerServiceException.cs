namespace TaskTrackerService.CustomExceptions;

public class TaskTrackerServiceException : Exception
{
    public TaskTrackerServiceException() { }

    public TaskTrackerServiceException(string message) : base(message) { }

    public TaskTrackerServiceException(string message,  Exception innerException) : base(message, innerException) { }

    public TaskTrackerServiceException(Exception innerException)
        : base("Internal error in AuthenticationService", innerException) { }
}