namespace Microservices.Core.Exceptions;

public class UnauthorizedDomainException : DomainException
{
    public UnauthorizedDomainException()
    {
        _message = "Unauthorized";
    }
    
    public override int Status => 403;   
}