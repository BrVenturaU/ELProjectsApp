using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace ELProjectsApp.WebApi.Middlewares;

public class GlobalErrorHandling(ILogger<GlobalErrorHandling> _logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        httpContext.Response.ContentType = "application/json";
        // If necessary add more than just 500 status code.
        httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        _logger.LogError($"Something went wrong: {exception}");
        await httpContext.Response.WriteAsJsonAsync(new
        {
            httpContext.Response.StatusCode,
            Message = "The server encountered an error and could nor complete your request.",
        }, cancellationToken);

        return true;
    }
}
