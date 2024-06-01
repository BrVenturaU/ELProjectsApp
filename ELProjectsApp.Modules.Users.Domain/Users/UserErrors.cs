using ELProjectsApp.Shared.Kernel;

namespace ELProjectsApp.Modules.Users.Domain.Users;

public static class UserErrors
{
    public static readonly Error NotFound = new("Users.NotFound", "The user was not found.");
    public static readonly Error RegistrationFailed = new("Users.RegistrationFailed",
        "The user creation has failed.");
    public static readonly Error TenantClaimAdditionFailed = new("Users.TenantClaimAdditionFailed",
        "The tenant claim could not be assigned to the user.");
    public static readonly Error InvalidUser = new("Users.InvalidUser", "User credentials are invalid.");


}
