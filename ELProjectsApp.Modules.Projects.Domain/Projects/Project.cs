namespace ELProjectsApp.Modules.Projects.Domain.Projects;
public class Project
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public int Duration { get; set; }
}