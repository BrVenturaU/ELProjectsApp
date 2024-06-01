using ELProjectsApp.Modules.Users.Application.Contracts;
using ELProjectsApp.Modules.Users.Domain.Users;
using ELProjectsApp.Shared.Kernel;
using ELProjectsApp.Shared.Kernel.Dtos.Organizations;
using ELProjectsApp.Shared.Kernel.Dtos.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace ELProjectsApp.Modules.Users.Infrastructure.Providers
{
    internal class AuthProvider(UserManager<IdentityUser<Guid>> _userManager, IConfiguration _configuration) : IAuthProvider
    {
        private const string _tenantClaim = "tenants";
        public async Task<Result<JwtTokenDto>> CreateToken(UserDto user)
        {
            var userDb = await _userManager.FindByEmailAsync(user.Email);
            if (userDb is null)
                return UserErrors.NotFound;

            var jwtSettings = _configuration.GetSection("JwtSettings");
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, userDb.Id.ToString()),
                new Claim(ClaimTypes.Email, userDb.Email)
            };

            var dbClaims = await _userManager.GetClaimsAsync(userDb);
            var tenants = dbClaims
                .Where(claim => claim.Type.Equals(_tenantClaim))
                .Select(claim => new TenantDto(claim.Value));

            claims.AddRange(dbClaims.Where(c => !c.Type.Equals(_tenantClaim)));
            claims.Add(new Claim(_tenantClaim, JsonSerializer.Serialize(tenants), JsonClaimValueTypes.Json));
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Secret"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddMinutes(double.Parse(jwtSettings["Expiration"]));
            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                issuer: jwtSettings["ValidIssuer"],
                audience: "",
                claims: claims,
                expires: expiration,
                signingCredentials: credentials
            );

            return new JwtTokenDto(new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken), expiration, tenants);
        }

        public async Task<Result> RegisterUser(UserRegistrationDto userRegistration)
        {
            var user = new IdentityUser<Guid>
            {
                UserName = userRegistration.Email,
                Email = userRegistration.Email
            };
            var result = await _userManager.CreateAsync(user, userRegistration.Password);
            return result.Succeeded ? Result.Success() : Result<bool, ErrorWithDetails>.Failure(
            new ErrorWithDetails(
                    UserErrors.RegistrationFailed.Code,
                    UserErrors.RegistrationFailed.Message,
                    result.Errors.Select(e => new Error(e.Code, e.Description)
                    )
                )
            ); 
        }

        public async Task<Result> AddUserTenantClaim(Guid userId, string slugTenant)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user is null)
                return UserErrors.NotFound;

            var result = await _userManager.AddClaimAsync(user, new Claim("tenants", slugTenant));

            return result.Succeeded ? Result.Success() : UserErrors.TenantClaimAdditionFailed;
        }

        public async Task<Result<UserDto>> ValidateUser(UserAuthenticationDto userAuthentication)
        {
            var user = await _userManager.FindByEmailAsync(userAuthentication.Email);
            if(user is null)
                return UserErrors.InvalidUser;
            var isValidPassword = await _userManager.CheckPasswordAsync(user, userAuthentication.Password);
            if(!isValidPassword)
                return UserErrors.InvalidUser;

            return new UserDto(user.Id, user.Email);
        }
    }
}
