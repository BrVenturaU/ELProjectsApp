using ELProjectsApp.Shared.Kernel;

namespace ELProjectsApp.Modules.Organizations.Domain.Organizations;

public static class OrganizationErrors
{
    public static Error NotBelongsToUser(Guid userId) => new Error("Organizations.NotBelongsToUser",
        $"The organization does not belongs to the user with id {userId} or not exists.");
}
