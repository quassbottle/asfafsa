namespace Microservices.Languages.Core.DTO;

public record LanguageCreateDTO
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public int LengthOfCourse { get; set; }
    public int UserId { get; set; }
}