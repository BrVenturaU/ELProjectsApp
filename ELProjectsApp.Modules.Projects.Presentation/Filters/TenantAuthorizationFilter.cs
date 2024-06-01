using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ELProjectsApp.Modules.Users.Api;
using ELProjectsApp.Modules.Organizations.Api;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace ELProjectsApp.Modules.Projects.Presentation.Filters;

public class TenantAuthorizationFilter(
    IUsersApi _usersApi, 
    IOrganizationsApi _organizationsApi
) : IAsyncAuthorizationFilter
{

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        if (!(await IsAuthorized(context.HttpContext)))
        {
            context.Result = new ForbidResult(JwtBearerDefaults.AuthenticationScheme);
        }
    }

    private async Task<bool> IsAuthorized(HttpContext context)
    {
        var slugTenant = context.Request.RouteValues
            .FirstOrDefault(kvp => kvp.Key.Equals("slugTenant"));
        var tenants = context.User.Claims
            .Where(c => c.Type.Equals("tenants"));
        var tenant = slugTenant.Value.ToString();
        var isTenantInUserClaims = await _usersApi.CheckTenantWithAuthUserTenantsClaim(tenant, tenants);
        var tenantBelongsToUser = await _organizationsApi.CheckTenantBelongsToAuthenticatedUser(tenant);
        return isTenantInUserClaims && tenantBelongsToUser; 
    }
}