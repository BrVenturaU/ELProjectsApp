using ELProjectsApp.Shared.Kernel;
using ELProjectsApp.Shared.Kernel.Dtos.Users;

namespace ELProjectsApp.Modules.Users.Application.Contracts;

public interface IAuthProvider
{
    Task<Result> RegisterUser(UserRegistrationDto userRegistration);
    Task<Result<UserDto>> ValidateUser(UserAuthenticationDto userAuthentication);
    Task<Result<JwtTokenDto>> CreateToken(UserDto user);
    Task<Result> AddUserTenantClaim(Guid userId, string slugTenant);
}
