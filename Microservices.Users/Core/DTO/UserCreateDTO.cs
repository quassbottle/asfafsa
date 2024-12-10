namespace Microservices.Users.Core.DTO;

public record UserCreateDTO
{
    public string Username { get; set; }
    public string Password { get; set;  }
};