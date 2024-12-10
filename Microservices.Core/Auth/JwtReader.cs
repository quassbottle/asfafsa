namespace Microservices.Core.Auth;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

public static class JwtReader
{
    public static string? GetId(string token)
    {
        return ParseToken(token, "id");
    }

    public static string? GetUsername(string token)
    {
        return ParseToken(token, "username");
    }

    private static string? ParseToken(string token, string role)
    {
        var removeBearer = token.Split(' ')[1];
        var handler = new JwtSecurityTokenHandler();
        var tokenData = handler.ReadJwtToken(removeBearer).Payload;
        return tokenData.Claims.FirstOrDefault(c => c.Type.Split('/').Last() == role)?.Value;
    }

    public static List<Claim> GetClaims(int id, string email, string role)
    {
        var claims = new List<Claim>
        {
            new("id", id.ToString()),
            new("username", email),
        };

        return claims;
    }
}