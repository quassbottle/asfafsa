namespace Microservices.Users.Core.DTO;

public record UserLoginDTO
{
    public string Username { get; set; }
    public string Password { get; set; }
}