using ELProjectsApp.Modules.Organizations.Domain.Contracts;
using ELProjectsApp.Modules.Organizations.Domain.Organizations;
using ELProjectsApp.Shared.Abstractions.Authentication;
using ELProjectsApp.Shared.Abstractions.Data;
using ELProjectsApp.Shared.Abstractions.Events;
using ELProjectsApp.Shared.Abstractions.Messaging;
using ELProjectsApp.Shared.Kernel.Dtos.Organizations;
using ELProjectsApp.Shared.Kernel.Events.Organizations;
using Microsoft.Extensions.DependencyInjection;

namespace ELProjectsApp.Modules.Organizations.Application.Organizations;

// Drop databases, add migrations, test everything, and if want add centralized response model
public class AddOrganization
{
    public record AddOrganizationCommand(string Name): ICommand<OrganizationDto>;
    internal class AddOrganizationCommandHandler(
        IOrganizationRepository _organizationRepository,
        IUserContext _userContext,
        [FromKeyedServices("OrganizationsUnitOfWork")] IUnitOfWork _unitOfWork,
        IEventBus _publisher
    ) : ICommandHandler<AddOrganizationCommand, OrganizationDto>
    {
        public async Task<OrganizationDto> Handle(AddOrganizationCommand request, CancellationToken cancellationToken)
        {
            var dbName = Guid.NewGuid().ToString();
            var organization = new Organization { 
                Name = request.Name, 
                UserId = _userContext.UserId,
                SlugTenant = dbName
            };

            _organizationRepository.Insert(organization);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _publisher
                .PublishAsync(new OrganizationCreatedEvent(organization.Id, organization.UserId, organization.SlugTenant), cancellationToken);
            return new OrganizationDto(organization.Id, organization.Name, organization.SlugTenant);
        }
    }
}