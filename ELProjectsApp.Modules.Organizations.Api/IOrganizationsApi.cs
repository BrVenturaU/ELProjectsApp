namespace ELProjectsApp.Modules.Organizations.Api;

public interface IOrganizationsApi
{
    Task<bool> CheckTenantBelongsToAuthenticatedUser(string tenant);
}
