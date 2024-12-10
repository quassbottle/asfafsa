namespace Microservices.Core.Auth.Models;

public record JwtModel
{
    public string Token { get; set; }
    public DateTime ExpiresAt { get; set; }
}