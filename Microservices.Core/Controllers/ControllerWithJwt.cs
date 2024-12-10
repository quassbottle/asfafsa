using Microservices.Core.Auth;
using Microservices.Core.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Microservices.Core.Controllers;

public class ControllerWithJwt: ControllerBase
{
    private string AuthHeader => HttpContext.Request.Headers.Authorization.ToString();

    protected int UserId
    {
        get
        {
            try
            {
                return int.Parse(JwtReader.GetId(AuthHeader)!);
            }
            catch
            {
                throw new UnauthorizedDomainException();
            }
        }
    }

    protected string Username  {
        get
        {
            try
            {
                return JwtReader.GetUsername(AuthHeader)!;
            }
            catch
            {
                throw new UnauthorizedDomainException();
            }
        }
    }

    protected void AssertUserIdIsSameAsAuth(int id)
    {
        if (UserId != id) throw new UnauthorizedDomainException();
    }
}