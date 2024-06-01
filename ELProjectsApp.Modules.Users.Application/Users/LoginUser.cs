using ELProjectsApp.Modules.Users.Application.Contracts;
using ELProjectsApp.Shared.Abstractions.Messaging;
using ELProjectsApp.Shared.Kernel;
using ELProjectsApp.Shared.Kernel.Dtos.Users;

namespace ELProjectsApp.Modules.Users.Application.Users;

public class LoginUser
{
    public record LoginUserCommand(string Email, string Password) : ICommand<Result<JwtTokenDto>>;

    internal class LoginUserCommandHandler(IAuthProvider _authProvider) : ICommandHandler<LoginUserCommand, Result<JwtTokenDto>>
    {
        public async Task<Result<JwtTokenDto>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var result = await _authProvider
                .ValidateUser(new UserAuthenticationDto(request.Email,request.Password));

            if(result.IsFailure) return result.Error;

            return await _authProvider.CreateToken(result.Value);
        }
    }
}
