using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WBAPI.Domain.Interfaces;
using WBAPI.Insfrastructure.Implementations.Settings;

namespace WBAPI.Insfrastructure.Implementations.Services
{
    public class JwtTokenServiceImp(IOptions<JwtSettings> opts) : IJwtTokenService
    {
        private readonly JwtSettings _settings = opts.Value;

        public string GenerateToken(string userId, string email, string username, IList<string> roles)
        {
            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, userId),
                new(JwtRegisteredClaimNames.Email,email),
                new(JwtRegisteredClaimNames.UniqueName, username),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this._settings.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _settings.Issuer,
                audience: _settings.Audience,
                claims: claims,
                expires: GetExpiration(),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public DateTime GetExpiration() =>
            DateTime.UtcNow.AddHours(this._settings.ExpirationHours);
    }
}
