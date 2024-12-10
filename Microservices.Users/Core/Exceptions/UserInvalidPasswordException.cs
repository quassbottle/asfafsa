using Microservices.Core.Exceptions;

namespace Microservices.Users.Core.Exceptions;

public class UserInvalidPasswordException : UnauthorizedDomainException
{
    public UserInvalidPasswordException()
    {
        _message = "Bad password";
    }
}