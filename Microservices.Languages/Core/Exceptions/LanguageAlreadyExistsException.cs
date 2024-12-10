using Microservices.Core.Exceptions;

namespace Microservices.Languages.Core.Exceptions;

public class LanguageAlreadyExistsException : BadRequestDomainException
{
    public LanguageAlreadyExistsException()
    {
        _message = "Such language already exists";
    }
}