using System.Net;
using MatchThree.Shared.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace MatchThree.API.ExceptionHandlers;

public class NoDataFoundExceptionHandler(IStringLocalizer<Localization> localization) 
    : IExceptionHandler
{
    private readonly IStringLocalizer<Localization> _localization = localization;

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        if (exception is not NoDataFoundException noDataFoundException) 
            return false;

        httpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;

        var localizedTittle = _localization[noDataFoundException.MessageKey];
        await httpContext.Response.WriteAsJsonAsync(new
            ProblemDetails //TODO fix body
        {
                Status = httpContext.Response.StatusCode,
                Type = exception.GetType().Name,
                Title = localizedTittle, 
                Detail = localizedTittle,
                Instance = $"{httpContext.Request.Method} " +
                           $"{httpContext.Request.Path}"
            }, cancellationToken: cancellationToken);

        return true;
    }
}