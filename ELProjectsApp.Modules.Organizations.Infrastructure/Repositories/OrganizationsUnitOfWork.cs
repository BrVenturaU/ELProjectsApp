using ELProjectsApp.Modules.Organizations.Infrastructure.Database;
using ELProjectsApp.Shared.Abstractions.Data;

namespace ELProjectsApp.Modules.Organizations.Infrastructure.Repositories;

internal class OrganizationsUnitOfWork (OrganizationsDbContext _dbContext) : IUnitOfWork
{
    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
