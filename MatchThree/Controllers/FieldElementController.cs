using MatchThree.API.Attributes;
using MatchThree.API.Models;
using MatchThree.BL.Configuration;
using MatchThree.Domain.Interfaces;
using MatchThree.Domain.Interfaces.FieldElement;
using MatchThree.Shared.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace MatchThree.API.Controllers;

[ApiController]
[Route("api/v1/users")]
public class FieldElementController(IReadFieldElementService readFieldElementService, 
    IUpdateFieldElementService updateFieldElementService,
    ITransactionService transactionService)
{
    private readonly IReadFieldElementService _readFieldElementService = readFieldElementService;
    private readonly IUpdateFieldElementService _updateFieldElementService = updateFieldElementService;
    private readonly ITransactionService _transactionService = transactionService;

    /// <summary>
    /// Gets field elements by user id
    /// </summary>
    [HttpGet("{userId:long}/field-elements")]
    [Authorize(Policy = AuthenticationConstants.UserIdPolicy)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Dictionary<int, int>))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
    [SwaggerOperation(OperationId = "GetFieldElements", Tags = ["FieldElements"])]
    public async Task<IResult> GetFieldElements(long userId, CancellationToken cancellationToken = new())
    {
        var fieldElements = await _readFieldElementService.GetByUserIdAsync(userId);

        return Results.Ok(fieldElements.ToDictionary(x => (int)x.Element,
            y => FieldElementsConfiguration.GetProfit(y.Element, y.Level)));
    }
    
    /// <summary>
    /// Upgrade field element by user identifier 
    /// </summary>
    [HttpPost("{userId:long}/field-elements/{fieldElement:int}/upgrade", Name = EndpointsConstants.UpgradeFieldElementEndpointName)]
    [Authorize(Policy = AuthenticationConstants.UserIdPolicy)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status402PaymentRequired, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
    [SwaggerOperation(OperationId = "UpgradeFieldElement", Tags = ["FieldElements"])]
    public async Task<IResult> UpgradeFieldElement([FromMultiSource] UpgradeFieldElementRequestDto request, 
        CancellationToken cancellationToken = new())
    {
        await _updateFieldElementService.UpgradeFieldElementAsync(request.UserId, request.CryptoType);
        await _transactionService.CommitAsync();
        return Results.NoContent();
    }
}