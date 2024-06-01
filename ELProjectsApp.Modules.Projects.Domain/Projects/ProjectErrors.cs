using ELProjectsApp.Shared.Kernel;

namespace ELProjectsApp.Modules.Projects.Domain.Projects;

public static class ProjectErrors
{
    public static readonly Error NotFound = new("Projects.NotFound", "The project was not found.");
}