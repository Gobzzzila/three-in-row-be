using System.Net;
using MatchThree.Shared.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace MatchThree.API.ExceptionHandlers;

public class MaxLevelReachedExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        //TODO Add logging??

        if (exception is not MaxLevelReachedException) 
            return false;

        httpContext.Response.StatusCode = (int)HttpStatusCode.Conflict;

        await httpContext.Response.WriteAsJsonAsync(new
            ProblemDetails //TODO fix body
            {
                Status = (int)HttpStatusCode.NotFound,
                Type = exception.GetType().Name,
                Title = exception.Message, 
                Detail = exception.Message,
                Instance = $"{httpContext.Request.Method} " +
                           $"{httpContext.Request.Path}"
            }, cancellationToken: cancellationToken);

        return true;
    }
}