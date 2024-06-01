using ELProjectsApp.Modules.Projects.Application.Contracts;
using ELProjectsApp.Shared.Abstractions.Events;
using ELProjectsApp.Shared.Kernel.Events.Organizations;

namespace ELProjectsApp.Modules.Projects.Application.Organizations;

internal class OrganizationCreated
{
    public sealed class OrganizationCreatedEventHandler(IDatabaseInitializer _databaseInitializer) : IEventHandler<OrganizationCreatedEvent>
    {
        public async Task Handle(OrganizationCreatedEvent notification, CancellationToken cancellationToken)
        {
            await _databaseInitializer.InitializeDatabase(notification.SlugTenant);
        }
    }
}
