using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Serialization;

namespace Microservices.Users.Core.Entities;

public class UserEntity
{
    private string _password;
    
    public int Id { get; set; }

    public string Username { get; set; }
    
    public ICollection<UserLanguageEntity> Languages { get; set; }

    [JsonIgnore]
    public string Password
    {
        get => _password;
        set => _password = HashPassword(value);
    }

    private static string HashPassword(string password)
    {
        return Encoding.UTF8.GetString(MD5.HashData(Encoding.UTF8.GetBytes(password)));
    }

    public bool PasswordEquals(string password)
    {
        return this._password == HashPassword(password);
    }
}