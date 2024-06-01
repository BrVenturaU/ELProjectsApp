using System.ComponentModel.DataAnnotations;

namespace ELProjectsApp.Modules.Organizations.Presentation.Requests;

public record OrganizationCreationRequest(
    [Required(ErrorMessage = "The organization's name is required.")]
    [MaxLength(30, ErrorMessage = "The organization's name must have a maximum of 30 characters.")]
    string Name
);