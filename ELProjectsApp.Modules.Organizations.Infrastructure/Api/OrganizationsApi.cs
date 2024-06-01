using ELProjectsApp.Modules.Organizations.Api;
using ELProjectsApp.Modules.Organizations.Infrastructure.Database;
using ELProjectsApp.Shared.Abstractions.Authentication;
using Microsoft.EntityFrameworkCore;

namespace ELProjectsApp.Modules.Organizations.Infrastructure.Api;

internal sealed class OrganizationsApi(OrganizationsDbContext _dbContext, IUserContext _userContext) : IOrganizationsApi
{
    public async Task<bool> CheckTenantBelongsToAuthenticatedUser(string tenant)
    {
        if (!_userContext.IsAuthenticated) return false;

        return await _dbContext
            .Organizations
            .AnyAsync(o => o.UserId.Equals(_userContext.UserId) && o.SlugTenant.Equals(tenant));
    }
}
