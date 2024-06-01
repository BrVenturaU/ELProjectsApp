using ELProjectsApp.Modules.Users.Application.Contracts;
using ELProjectsApp.Shared.Abstractions.Events;
using ELProjectsApp.Shared.Kernel.Events.Organizations;

namespace ELProjectsApp.Modules.Users.Application.Organizations;

internal class AddUserOrganizationCreatedSlug
{
    public sealed class OrganizationCreatedEventHandler(IAuthProvider _authProvider) : IEventHandler<OrganizationCreatedEvent>
    {
        public async Task Handle(OrganizationCreatedEvent notification, CancellationToken cancellationToken)
        {
            await _authProvider.AddUserTenantClaim(notification.UserId, notification.SlugTenant);
        }
    }
}
