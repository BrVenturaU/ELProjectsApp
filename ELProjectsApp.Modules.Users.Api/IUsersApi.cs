using System.Security.Claims;

namespace ELProjectsApp.Modules.Users.Api;

public interface IUsersApi
{
    Task<bool> CheckTenantWithAuthUserTenantsClaim(string tenant, IEnumerable<Claim>? tenantClaims);
}
