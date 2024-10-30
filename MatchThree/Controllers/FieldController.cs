using MatchThree.API.Attributes;
using MatchThree.API.Models;
using MatchThree.Domain.Interfaces;
using MatchThree.Domain.Interfaces.Field;
using MatchThree.Shared.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace MatchThree.API.Controllers;

[ApiController]
[Route("api/v1/fields")]
public class FieldController(IUpdateFieldService updateFieldService,
    IReadFieldService readFieldService,
    ITransactionService transactionService,
    IMoveService moveService)
{
    private readonly IUpdateFieldService _updateFieldService = updateFieldService;
    private readonly IReadFieldService _readFieldService = readFieldService;
    private readonly ITransactionService _transactionService = transactionService;
    private readonly IMoveService _moveService = moveService;

    /// <summary>
    /// Gets field by user id
    /// </summary>
    [HttpGet("{userId:long}")]
    [Authorize(Policy = AuthenticationConstants.UserIdPolicy)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int[][]))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
    [SwaggerOperation(OperationId = "GetField", Tags = ["Field"])]
    public async Task<IResult> GetField(long userId, CancellationToken cancellationToken = new())
    {
        var fieldEntity = await _readFieldService.GetByUserIdAsync(userId);
        return Results.Ok(fieldEntity.Field);
    }
    
    /// <summary>
    /// Upgrade field by user identifier 
    /// </summary>
    [HttpPost("{userId:long}/upgrade-field", Name = EndpointsConstants.UpgradeFieldEndpointName)]
    [Authorize(Policy = AuthenticationConstants.UserIdPolicy)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status402PaymentRequired, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
    [SwaggerOperation(OperationId = "UpgradeField", Tags = ["Field"])]
    public async Task<IResult> UpgradeField(long userId, CancellationToken cancellationToken = new())
    {
        await _updateFieldService.UpgradeFieldAsync(userId);
        await _transactionService.Commit();
        return Results.NoContent();
    }
    
    /// <summary>
    /// Saves move
    /// </summary>
    [HttpPost("{userId:long}/move")]
    [Authorize(Policy = AuthenticationConstants.UserIdPolicy)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
    [SwaggerOperation(OperationId = "MakeMove", Tags = ["Field"])]
    public async Task<IResult> MakeMove([FromMultiSource]MakeMoveRequestDto request, 
        CancellationToken cancellationToken = new())
    {
        await _moveService.MakeMoveAsync(request.UserId, (uint)request.Body.Reward, request.Body.Field, request.Body.Hash);
        await _transactionService.Commit();
        return Results.NoContent();
    }
}