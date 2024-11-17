using AutoMapper;
using MatchThree.API.Models;
using MatchThree.Domain.Interfaces;
using MatchThree.Domain.Interfaces.DailyLogin;
using MatchThree.Shared.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace MatchThree.API.Controllers;

[ApiController]
[Route("api/v1/users")]
public class DailyLoginController(IReadDailyLoginService readDailyLoginService,
    IExecuteDailyLoginService executeDailyLoginService,
    ITransactionService transactionService,
    IMapper mapper)
{
    private readonly IReadDailyLoginService _readDailyLoginService = readDailyLoginService;
    private readonly IExecuteDailyLoginService _executeDailyLoginService = executeDailyLoginService;
    private readonly ITransactionService _transactionService = transactionService;
    private readonly IMapper _mapper = mapper;

    /// <summary>
    /// Get daily login info by user identifier 
    /// </summary>
    [HttpGet("{userId:long}/daily-login")]
    [Authorize(Policy = AuthenticationConstants.UserIdPolicy)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DailyLoginDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
    [SwaggerOperation(OperationId = "GetDailyLoginInfo", Tags = ["DailyLogin"])]
    public async Task<IResult> GetDailyLoginInfo(long userId, CancellationToken cancellationToken = new())
    {
        var entity = await _readDailyLoginService.GetDailyLoginInfoByUserIdAsync(userId);
        return Results.Ok(_mapper.Map<DailyLoginDto>(entity));
    }
    
    /// <summary>
    /// Execute daily login by user identifier 
    /// </summary>
    [HttpPost("{userId:long}/daily-login")]
    [Authorize(Policy = AuthenticationConstants.UserIdPolicy)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
    [SwaggerOperation(OperationId = "ExecuteDailyLogin", Tags = ["DailyLogin"])]
    public async Task<IResult> ExecuteDailyLogin(long userId, CancellationToken cancellationToken = new())
    {
        await _executeDailyLoginService.ExecuteDailyLogin(userId);
        await _transactionService.CommitAsync();
        return Results.NoContent();
    }
}