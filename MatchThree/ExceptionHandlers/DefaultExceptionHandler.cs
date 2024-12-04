using System.Net;
using MatchThree.Shared.Constants;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace MatchThree.API.ExceptionHandlers;

public class DefaultExceptionHandler(ILogger<DefaultExceptionHandler> logger, 
    IStringLocalizer<Localization> localization) 
    : IExceptionHandler
{
    private readonly ILogger<DefaultExceptionHandler> _logger = logger;
    private readonly IStringLocalizer<Localization> _localization = localization;

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception,
        CancellationToken cancellationToken)
    {
        _logger.LogError(exception, $"Unhandled exception for path {httpContext.Request.Path}");
        
        httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        var localizedTittle = _localization[TranslationConstants.ExceptionDefaultTextKey];
        
        await httpContext.Response.WriteAsJsonAsync(new ProblemDetails
            {
                Status = httpContext.Response.StatusCode,
                Type = exception.GetType().Name,
                Title = localizedTittle,
                Detail = localizedTittle,
                Instance = $"{httpContext.Request.Method} {httpContext.Request.Path}"
            },
            cancellationToken: cancellationToken);

        return true;
    }
}