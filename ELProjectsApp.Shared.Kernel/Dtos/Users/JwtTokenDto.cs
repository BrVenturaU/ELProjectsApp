using ELProjectsApp.Shared.Kernel.Dtos.Organizations;

namespace ELProjectsApp.Shared.Kernel.Dtos.Users;

public record JwtTokenDto(string Token, DateTime Expiration, IEnumerable<TenantDto> Tenants);
