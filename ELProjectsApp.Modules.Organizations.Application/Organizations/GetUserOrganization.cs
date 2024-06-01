using ELProjectsApp.Modules.Organizations.Domain.Contracts;
using ELProjectsApp.Modules.Organizations.Domain.Organizations;
using ELProjectsApp.Shared.Abstractions.Authentication;
using ELProjectsApp.Shared.Abstractions.Messaging;
using ELProjectsApp.Shared.Kernel;
using ELProjectsApp.Shared.Kernel.Dtos.Organizations;

namespace ELProjectsApp.Modules.Organizations.Application.Organizations;

public class GetUserOrganization
{
    public record GetUserOrganizationQuery(Guid Id) : IQuery<Result<OrganizationDto>>;

    internal sealed class GetUserOrganizationQueryHandler(
        IOrganizationRepository _organizationRepository,
        IUserContext _userContex
    ) : IQueryHandler<GetUserOrganizationQuery, Result<OrganizationDto>>
    {
        public async Task<Result<OrganizationDto>> Handle(GetUserOrganizationQuery request, CancellationToken cancellationToken)
        {
            var organization = await _organizationRepository.GetUserOrganizationById(_userContex.UserId, request.Id);
            if (organization is null)
                return OrganizationErrors.NotBelongsToUser(_userContex.UserId);

            return new OrganizationDto(organization.Id, organization.Name, organization.SlugTenant);
        }
    }
}
