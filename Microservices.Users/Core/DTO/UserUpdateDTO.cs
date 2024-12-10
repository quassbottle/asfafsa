namespace Microservices.Users.Core.DTO;

public record UserUpdateDTO
{
    public string Password { get; set; }
};