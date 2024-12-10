using System.Text.Json.Serialization;

namespace Microservices.Users.Core.Entities;

public class UserLanguageEntity
{
    public int Id { get; set; }
    
    public int UserId { get; set; }
    public int LanguageId { get; set; }
    
    [JsonIgnore]
    public UserEntity User { get; set; }
}