using System.Security.Claims;
using Microservices.Core.Auth.Models;

namespace Microservices.Users.Core.Services;

public interface IJwtService
{
    public JwtModel CreateToken(IEnumerable<Claim> claims, int tokenExpiresAfterHours = 0);
}