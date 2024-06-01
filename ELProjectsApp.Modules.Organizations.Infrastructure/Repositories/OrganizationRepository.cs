using ELProjectsApp.Modules.Organizations.Domain.Contracts;
using ELProjectsApp.Modules.Organizations.Domain.Organizations;
using ELProjectsApp.Modules.Organizations.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace ELProjectsApp.Modules.Organizations.Infrastructure.Repositories;

internal class OrganizationRepository(OrganizationsDbContext _dbContext) : IOrganizationRepository
{
    public async Task<Organization?> GetUserOrganizationById(Guid userId, Guid id)
    {
        return await _dbContext.Organizations.FirstOrDefaultAsync(o => o.UserId.Equals(userId) && o.Id == id);
    }

    public async Task<IEnumerable<Organization>> GetUserOrganizations(Guid userId)
    {
        return await _dbContext.Organizations.Where(o => o.UserId.Equals(userId)).ToListAsync();
    }

    public void Insert(Organization organization)
    {
        _dbContext.Organizations.Add(organization);
    }
}
