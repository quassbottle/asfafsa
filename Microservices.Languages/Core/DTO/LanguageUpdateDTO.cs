namespace Microservices.Languages.Core.DTO;

public record LanguageUpdateDTO
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int LenghtOfCourse { get; set; }
}