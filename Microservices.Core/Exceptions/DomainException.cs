namespace Microservices.Core.Exceptions;

public class DomainException : Exception
{
    protected string _message = "Internal server error";

    public virtual int Status { get; } = 500;
    public string Message => _message;
    
    protected DomainException() {}
}