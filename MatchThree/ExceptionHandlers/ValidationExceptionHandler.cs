﻿using System.Net;
using MatchThree.Shared.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace MatchThree.API.ExceptionHandlers;

public class ValidationExceptionHandler(IStringLocalizer<Localization> localization) 
    : IExceptionHandler
{
    private readonly IStringLocalizer<Localization> _localization = localization;

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, 
        CancellationToken cancellationToken)
    {
        if (exception is not ValidationException validationException) 
            return false;

        httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        var localizedTittle = string.Format(_localization[validationException.MessageKey], validationException.MessageArgs);
        
        await httpContext.Response.WriteAsJsonAsync(new ProblemDetails
        {
            Status = httpContext.Response.StatusCode,
            Type = exception.GetType().Name,
            Title = localizedTittle, 
            Detail = localizedTittle,
            Instance = $"{httpContext.Request.Method} {httpContext.Request.Path}"
        }, cancellationToken: cancellationToken);

        return true;
    }
}