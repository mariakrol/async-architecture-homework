using System.ComponentModel.DataAnnotations;
using AuthenticationService.Data.Storage;

namespace AuthenticationService.Data.RequestResponseModels.User;

public class UserCreationRequest
{
    [Required]
    public string? UserName { get; set; }

    [Required]
    public string? Password { get; set; }

    [Required]
    public Role? Role { get; set; }
}