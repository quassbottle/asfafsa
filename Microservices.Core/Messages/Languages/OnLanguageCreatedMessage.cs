namespace Microservices.Core.Messages.Languages;

public class OnLanguageCreatedMessage
{
    public int LanguageId { get; set; }
    public int UserId { get; set; }
}