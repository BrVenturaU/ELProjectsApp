using ELProjectsApp.Modules.Projects.Domain.Contracts;
using ELProjectsApp.Modules.Projects.Domain.Projects;
using ELProjectsApp.Modules.Projects.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace ELProjectsApp.Modules.Projects.Infrastructure.Repositories;

internal class ProjectRepository(ProjectsDbContext _dbContext) : IProjectRepository
{
    public async Task<IEnumerable<Project>> GetAllProjects()
    {
        return await _dbContext.Projects.ToListAsync();
    }

    public Task<Project?> GetProjectById(Guid id)
    {
        return _dbContext.Projects.SingleOrDefaultAsync(p => p.Id == id);
    }

    public void Insert(Project project)
    {
        _dbContext.Projects.Add(project);
    }
}
