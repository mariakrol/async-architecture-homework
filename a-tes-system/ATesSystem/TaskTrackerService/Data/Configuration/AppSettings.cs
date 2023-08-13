namespace TaskTrackerService.Data.Configuration;

public class AppSettings
{
    public string? PasswordEncryptionSecret { get; set; }

    public string? TokenSignSecret { get; set; }
}