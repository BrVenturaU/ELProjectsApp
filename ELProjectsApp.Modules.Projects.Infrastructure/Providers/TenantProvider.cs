using ELProjectsApp.Modules.Projects.Application.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace ELProjectsApp.Modules.Projects.Infrastructure.Providers;

internal class TenantProvider(IHttpContextAccessor _httpContextAccessor, IConfiguration _configuration) : ITenantProvider
{
    public string? SlugTenant => 
        _httpContextAccessor
        .HttpContext?
        .Request
        .RouteValues
        .FirstOrDefault(kvp => kvp.Key.Equals("slugTenant"))
        .Value?
        .ToString();

    public string? GetTenantConnectionString()
    {
        // The ConnectionString should be placed in a more secure and decentralized place.
        var defaultConnectionString = _configuration
                .GetConnectionString("Default");
        return !string.IsNullOrEmpty(SlugTenant) ? 
            defaultConnectionString?.Replace("TenantDefaultDb", SlugTenant) :
            defaultConnectionString;
    }

    public string? GetTenantConnectionString(string slugTenant)
    {
        var defaultConnectionString = _configuration
                .GetConnectionString("Default");
        return !string.IsNullOrEmpty(SlugTenant) ?
            defaultConnectionString?.Replace("TenantDefaultDb", SlugTenant) :
            defaultConnectionString?.Replace("TenantDefaultDb", slugTenant);
    }


}
