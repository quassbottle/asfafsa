namespace Microservices.Languages.Core.Entities;

public class LanguageEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public int LenghtOfCourse { get; set; }

    public int UserId { get; set; }
    public string? Status { get; set; }
}