namespace ELProjectsApp.Shared.Abstractions.Authentication;

public interface IUserContext
{
    bool IsAuthenticated { get; }
    Guid UserId { get; }
}
