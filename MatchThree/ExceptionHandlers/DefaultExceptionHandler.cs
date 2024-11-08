using System.Net;
using MatchThree.Shared.Constants;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace MatchThree.API.ExceptionHandlers;

public class DefaultExceptionHandler(IStringLocalizer<Localization> localization) 
    : IExceptionHandler
{
    private readonly IStringLocalizer<Localization> _localization = localization;

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        await httpContext.Response.WriteAsJsonAsync(new
            ProblemDetails
            {
                Status = httpContext.Response.StatusCode,
                Type = exception.GetType().Name,
                Title = _localization[TranslationConstants.ExceptionDefaultTextKey], 
                Detail = exception.Message,
                Instance = $"{httpContext.Request.Method} " +
                           $"{httpContext.Request.Path}"
            }, cancellationToken: cancellationToken);

        return true;
    }
}