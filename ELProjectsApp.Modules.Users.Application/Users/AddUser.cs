using ELProjectsApp.Modules.Users.Application.Contracts;
using ELProjectsApp.Shared.Abstractions.Messaging;
using ELProjectsApp.Shared.Kernel;
using ELProjectsApp.Shared.Kernel.Dtos.Users;

namespace ELProjectsApp.Modules.Users.Application.Users;
public class AddUser
{
    public record AddUserCommand(string Email, string Password): ICommand<Result>;

    internal class AddUserCommandHandler(IAuthProvider _authProvider) : ICommandHandler<AddUserCommand, Result>
    {
        public Task<Result> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            return _authProvider.RegisterUser(new UserRegistrationDto(request.Email, request.Password));
        }
    }
}
