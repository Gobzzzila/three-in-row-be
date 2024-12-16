using MatchThree.Domain.Interfaces;
using MatchThree.Domain.Interfaces.User;
using MatchThree.Shared.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace MatchThree.API.Controllers;

[ApiController]
[Route("api/v1/users")]
public class AdController(IReadUserService readUserService,
    IUpdateUserService updateUserService,
    ITransactionService transactionService)
{
    private readonly IReadUserService _readUserService = readUserService;
    private readonly IUpdateUserService _updateUserService = updateUserService;
    private readonly ITransactionService _transactionService = transactionService;

    /// <summary>
    /// Gets number of remaining ads today by user id
    /// </summary>
    [HttpGet("{userId:long}/ads")]
    [Authorize(Policy = AuthenticationConstants.UserIdPolicy)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
    [SwaggerOperation(OperationId = "GetDailyAdsAmount", Tags = ["Ad"])]
    public async Task<IResult> GetDailyAdsAmount(long userId, CancellationToken cancellationToken = new())
    {
        var dailyAdsAmount = await _readUserService.GetDailyAdsAmountAsync(userId);
        return Results.Ok(dailyAdsAmount);
    }
    
    /// <summary>
    /// Records the viewing of ads
    /// </summary>
    [HttpPost("{userId:long}/ads")]
    [Authorize(Policy = AuthenticationConstants.UserIdPolicy)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    [SwaggerOperation(OperationId = "GetAdsReward", Tags = ["Ad"])]
    public async Task<IResult> GetAdsReward(long userId, CancellationToken cancellationToken = new())
    {
        await _updateUserService.RecordAdViewAsync(userId);
        await _transactionService.CommitAsync();
        return Results.NoContent();
    }
}