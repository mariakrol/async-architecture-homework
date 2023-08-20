using System.ComponentModel.DataAnnotations;

namespace AuthenticationService.Data.RequestResponseModels.Authentication;

public class AuthenticationRequest
{
    [Required]
    public string? UserName { get; set; }

    [Required]
    public string? Password { get; set; }
}