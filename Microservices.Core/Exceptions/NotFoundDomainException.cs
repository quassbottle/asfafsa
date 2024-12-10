namespace Microservices.Core.Exceptions;

public class NotFoundDomainException : DomainException
{
    protected NotFoundDomainException()
    {
        _message = "Not found";
    }
    
    public override int Status => 404;
}