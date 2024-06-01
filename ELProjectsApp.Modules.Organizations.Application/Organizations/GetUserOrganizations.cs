using ELProjectsApp.Modules.Organizations.Domain.Contracts;
using ELProjectsApp.Shared.Abstractions.Authentication;
using ELProjectsApp.Shared.Abstractions.Messaging;
using ELProjectsApp.Shared.Kernel.Dtos.Organizations;

namespace ELProjectsApp.Modules.Organizations.Application.Organizations;

public class GetUserOrganizations
{
    public record GetUserOrganizationsQuery() : IQuery<IEnumerable<OrganizationDto>>;

    internal class GetUserOrganizationsQueryHandler(
        IOrganizationRepository _organizationRepository,
        IUserContext _userContex
    ) : IQueryHandler<GetUserOrganizationsQuery, IEnumerable<OrganizationDto>>
    {
        public async Task<IEnumerable<OrganizationDto>> Handle(GetUserOrganizationsQuery request, CancellationToken cancellationToken)
        {
            var organizations = await _organizationRepository.GetUserOrganizations(_userContex.UserId);
            return organizations.Select(o => new OrganizationDto(o.Id, o.Name, o.SlugTenant));
        }
    }
}
