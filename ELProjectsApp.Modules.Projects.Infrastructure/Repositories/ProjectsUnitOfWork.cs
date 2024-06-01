using ELProjectsApp.Modules.Projects.Infrastructure.Database;
using ELProjectsApp.Shared.Abstractions.Data;

namespace ELProjectsApp.Modules.Projects.Infrastructure.Repositories;

internal class ProjectsUnitOfWork(ProjectsDbContext _dbContext) : IUnitOfWork
{
    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
