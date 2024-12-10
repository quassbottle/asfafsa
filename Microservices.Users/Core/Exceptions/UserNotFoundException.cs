using Microservices.Core.Exceptions;

namespace Microservices.Users.Core.Exceptions;

public class UserNotFoundException : NotFoundDomainException
{
    public UserNotFoundException()
    {
        _message = "User not found";
    }
}