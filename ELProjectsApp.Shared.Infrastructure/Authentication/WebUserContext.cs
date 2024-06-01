using ELProjectsApp.Shared.Abstractions.Authentication;
using ELProjectsApp.Shared.Infrastructure.Extensions;
using Microsoft.AspNetCore.Http;

namespace ELProjectsApp.Shared.Infrastructure.Authentication;

internal sealed class WebUserContext(IHttpContextAccessor _httpContextAccessor) : IUserContext
{
    public Guid UserId =>
        _httpContextAccessor
            .HttpContext?
            .User
            .GetUserId() ??
        throw new ApplicationException("User context is unavailable");

    public bool IsAuthenticated =>
        _httpContextAccessor
            .HttpContext?
            .User
            .Identity?
            .IsAuthenticated ??
        throw new ApplicationException("User context is unavailable");
}
