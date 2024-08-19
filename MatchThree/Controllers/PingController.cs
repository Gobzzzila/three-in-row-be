using Microsoft.AspNetCore.Mvc;

namespace MatchThree.API.Controllers;

[ApiController]
public class PingController : ControllerBase
{
    /// <summary>
    /// Ping
    /// </summary>
    [HttpGet("/ping")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IResult Get()
    {
        return Results.Ok();
    }
}