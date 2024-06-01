using System.ComponentModel.DataAnnotations;

namespace ELProjectsApp.Modules.Projects.Presentation.Requests;

public record ProjectCreationRequest
{
    [Required(ErrorMessage = "The project's name is required.")]
    [MaxLength(30, ErrorMessage = "The project's name must have a maximum of 30 characters.")]
    public string Name { get; init; }
    [Required]
    [MaxLength(200, ErrorMessage = "The project's description must have a maximum of 200 characters.")]
    public string Description { get; init; }
    [Required]
    public int Duration { get; init; }
}
