using ELProjectsApp.Modules.Organizations.Domain.Organizations;

namespace ELProjectsApp.Modules.Organizations.Domain.Contracts;

public interface IOrganizationRepository
{
    Task<Organization?> GetUserOrganizationById(Guid userId, Guid id); 
    Task<IEnumerable<Organization>> GetUserOrganizations(Guid userId);
    void Insert(Organization organization);
}
