using ELProjectsApp.Modules.Users.Api;
using ELProjectsApp.Shared.Abstractions.Authentication;
using ELProjectsApp.Shared.Kernel.Dtos.Organizations;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace ELProjectsApp.Modules.Users.Infrastructure.Api;

internal sealed class UsersApi(UserManager<IdentityUser<Guid>> _userManager, IUserContext _userContext) : IUsersApi
{
    public async Task<bool> CheckTenantWithAuthUserTenantsClaim(string tenant, IEnumerable<Claim>? tenantClaims)
    {
        if (!_userContext.IsAuthenticated) return false;

        var isInTenantClaims = tenantClaims?
            .Where(c => c.Type.Equals("tenants"))
            .Select(c => new TenantDto(c.Value))
            .Any(t => t.SlugTenant.Equals(tenant)) ?? false;
        if(isInTenantClaims) return true;

        var user = await _userManager.FindByIdAsync(_userContext.UserId.ToString());
        if(user is null) return false;

        var userClaims = await _userManager.GetClaimsAsync(user);

        return userClaims.Any(c => c.Type.Equals("tenants") && c.Value.Equals(tenant));
    }
}
