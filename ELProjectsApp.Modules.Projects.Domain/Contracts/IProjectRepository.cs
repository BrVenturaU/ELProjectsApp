using ELProjectsApp.Modules.Projects.Domain.Projects;

namespace ELProjectsApp.Modules.Projects.Domain.Contracts;

public interface IProjectRepository
{
    Task<IEnumerable<Project>> GetAllProjects();
    Task<Project?> GetProjectById(Guid id);
    void Insert(Project project);
}