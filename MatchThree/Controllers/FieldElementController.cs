using AutoMapper;
using MatchThree.API.Models;
using MatchThree.BL.Configuration;
using MatchThree.Domain.Interfaces.FieldElement;
using MatchThree.Shared.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace MatchThree.API.Controllers;

[ApiController]
[Route("api/v1/field-elements")]
public class FieldElementController(IReadFieldElementService readFieldElementService)
{
    private readonly IReadFieldElementService _readFieldElementService = readFieldElementService;

    /// <summary>
    /// Gets field elements by user id
    /// </summary>
    [HttpGet("{userId:long}")]
    [Authorize(Policy = AuthenticationConstants.UserIdPolicy)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Dictionary<int, int>))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
    [SwaggerOperation(OperationId = "GetFieldElements", Tags = ["FieldElements"])]
    public async Task<IResult> GetFieldElements(long userId, CancellationToken cancellationToken = new())
    {
        var fieldElements = await _readFieldElementService.GetByUserIdAsync(userId);

        return Results.Ok(fieldElements.ToDictionary(x => Convert.ToInt32(x.Element),
            y => FieldElementsConfiguration.GetProfit(y.Element, y.Level)));
    }
}