namespace Microservices.Core.Exceptions;

public class BadRequestDomainException : DomainException
{
    protected BadRequestDomainException()
    {
        _message = "Bad request";
    }
    
    public override int Status => 400;
}