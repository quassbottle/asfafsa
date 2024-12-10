using Microservices.Core.Exceptions;

namespace Microservices.Users.Core.Exceptions;

public class UserAlreadyExistsException : BadRequestDomainException
{
    public UserAlreadyExistsException()
    {
        _message = "Such user already exists";
    }
}