using Microservices.Core.Exceptions;

namespace Microservices.Languages.Core.Exceptions;

public class LanguageNotFoundException : NotFoundDomainException
{
    public LanguageNotFoundException()
    {
        _message = "Language not found";
    }
}