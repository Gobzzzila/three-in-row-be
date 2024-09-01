using MatchThree.API.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace MatchThree.API.Controllers;

[ApiController]
public class PingController(IStringLocalizer<Localization> localization) : ControllerBase
{
    private readonly IStringLocalizer<Localization> _localization = localization;

    /// <summary>
    /// Ping
    /// </summary>
    [HttpGet("/ping")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IResult Get()
    {
        var text = _localization["PingOk"];
        return Results.Ok(text.Value);
    }
}