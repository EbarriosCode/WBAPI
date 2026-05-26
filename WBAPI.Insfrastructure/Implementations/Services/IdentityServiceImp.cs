using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using WBAPI.Application.Features.Auth.DTOs;
using WBAPI.Application.Interfaces;
using WBAPI.Domain.Common;
using WBAPI.Domain.Interfaces;
using WBAPI.Insfrastructure.Implementations.Identity;
using WBAPI.Insfrastructure.Implementations.Settings;

namespace WBAPI.Insfrastructure.Implementations.Services
{
    public class IdentityServiceImp(
        UserManager<AppUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IJwtTokenService jwtTokenService,
        IOptions<IdentityRolesSettings> rolesOptions
        ) : IIdentityService
    {
        private readonly IdentityRolesSettings _roles = rolesOptions.Value;

        public async Task<DomainResult<AuthResponseDto>> RegisterAsync(string username, string email, string password, CancellationToken cancellationToken = default)
        {
            if (await userManager.FindByEmailAsync(email) is not null)
                return DomainResult<AuthResponseDto>.Failure("The email has already been registered.");

            var user = new AppUser
            {
                UserName = username,
                Email = email,
                DisplayName = username,
                EmailConfirmed = true
            };

            // Identity hashea la contraseña automáticamente (PBKDF2)
            var createResult = await userManager.CreateAsync(user, password);

            if (!createResult.Succeeded)
                return DomainResult<AuthResponseDto>.Failure(createResult.Errors.Select(e => e.Description).ToArray());

            await EnsureRoleExistsAsync(this._roles.DefaultRole);
            await userManager.AddToRoleAsync(user, this._roles.DefaultRole);

            return await BuildAuthResponseAsync(user);
        }

        public async Task<DomainResult<AuthResponseDto>> LoginAsync(string email, string password, CancellationToken cancellationToken = default)
        {
            var user = await userManager.FindByEmailAsync(email);

            if (user is null || !user.IsActive)
                return DomainResult<AuthResponseDto>.Failure("Invalid credentials.");

            return await BuildAuthResponseAsync(user);
        }        

        private async Task<DomainResult<AuthResponseDto>> BuildAuthResponseAsync(AppUser user)
        {
            var roles = await userManager.GetRolesAsync(user);
            var token = jwtTokenService.GenerateToken(user.Id, user.Email!, user.UserName!, roles);

            var response = new AuthResponseDto(
                user.Id,
                user.UserName!,
                user.Email!,
                roles.FirstOrDefault() ?? this._roles.DefaultRole,
                token,
                jwtTokenService.GetExpiration());

            return DomainResult<AuthResponseDto>.Success(response);
        }

        private async Task EnsureRoleExistsAsync(string role)
        {
            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole(role));
        }
    }
}
