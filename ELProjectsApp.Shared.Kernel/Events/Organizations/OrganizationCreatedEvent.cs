using ELProjectsApp.Shared.Abstractions.Events;

namespace ELProjectsApp.Shared.Kernel.Events.Organizations;

public record OrganizationCreatedEvent(Guid Id, Guid UserId, string SlugTenant) : Event(Id);