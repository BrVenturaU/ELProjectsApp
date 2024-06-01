namespace ELProjectsApp.Modules.Projects.Application.Contracts;

public interface ITenantProvider
{
    string? SlugTenant { get; }

    string? GetTenantConnectionString();
    string? GetTenantConnectionString(string slugTenant);
}
