using MatchThree.API.Authentication.Interfaces;
using MatchThree.Shared.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace MatchThree.API.Authentication;

public class JwtTokenExpiredHandler(IStringLocalizer<Localization> localization) : IJwtTokenExpiredHandler
{
    private readonly IStringLocalizer<Localization> _localization = localization;

    public void HandleAsync(HttpContext context)
    {
        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        var localizedTittle = _localization[TranslationConstants.ExceptionValidationTextKey];
        
        context.Response.Headers.WWWAuthenticate = "Bearer error=\"invalid_token\",error_description=\"The token expired\"";
        context.Response.WriteAsJsonAsync(new ProblemDetails
        {
            Status = context.Response.StatusCode,
            Type = "SecurityTokenExpiredException",
            Title = localizedTittle, 
            Detail = localizedTittle,
            Instance = $"{context.Request.Method} {context.Request.Path}"
        });
    }
}