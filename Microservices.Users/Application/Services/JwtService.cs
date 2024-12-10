using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microservices.Core.Auth;
using Microservices.Core.Auth.Models;
using Microservices.Users.Core.Services;
using Microsoft.IdentityModel.Tokens;

namespace Microservices.Users.Application.Services;

public class JwtService(JwtSettings settings) : IJwtService
{
    public JwtModel CreateToken(IEnumerable<Claim> claims, int tokenExpiresAfterHours = 0)
    {
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.Key));

        if (tokenExpiresAfterHours == 0)
        {
            tokenExpiresAfterHours = settings.TokenExpiresAfterHours;
        }

        var expiresAt = DateTime.UtcNow.AddHours(tokenExpiresAfterHours);

        var token = new JwtSecurityToken(settings.Issuer, settings.Audience, claims, null, expiresAt,
            new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));

        return
            new JwtModel
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ExpiresAt = expiresAt
            };
    }
}