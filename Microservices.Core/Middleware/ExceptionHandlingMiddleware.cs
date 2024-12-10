using System.Text.Json;
using Microservices.Core.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Microservices.Core.Middleware;

public class ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            logger.LogError(e, e.Message);

            await HandleExceptionAsync(context, e);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
    {
        httpContext.Response.ContentType = "application/json";

        httpContext.Response.StatusCode = exception switch
        {
            DomainException domainException => domainException.Status,
            _ => StatusCodes.Status500InternalServerError
        };

        var response = new
        {
            status = httpContext.Response.StatusCode,
            message = exception is DomainException de ? de.Message : "Internal server error",
            timestamp = DateTime.Now
        };

        await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}