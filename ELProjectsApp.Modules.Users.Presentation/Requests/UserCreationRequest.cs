using System.ComponentModel.DataAnnotations;

namespace ELProjectsApp.Modules.Users.Presentation.Dtos;

public record UserCreationRequest
{
    [Required(ErrorMessage = "The email is required.")]
    public string Email { get; init; }
    [Required(ErrorMessage = "The password is required.")]
    public string Password { get; init; }
    [Required(ErrorMessage = "The password confirmation is required.")]
    [Compare("Password", ErrorMessage = "Passwords must match.")]
    public string PasswordConfirmation { get; init; }
};
