using AutoMapper;
using MatchThree.API.Models;
using MatchThree.Domain.Interfaces;
using MatchThree.Domain.Interfaces.UserSettings;
using MatchThree.Shared.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace MatchThree.API.Controllers;

[ApiController]
[Route("api/v1/users")]
public class UserSettingsController(IReadUserSettingsService readUserSettingsService,
    // IUpdateUserService updateUserService,
    ITransactionService transactionService,
    IMapper mapper)
{
    private readonly IReadUserSettingsService _readUserSettingsService = readUserSettingsService;
    private readonly ITransactionService _transactionService = transactionService;
    private readonly IMapper _mapper = mapper;

    /// <summary>
    /// Get user settings by user identifier 
    /// </summary>
    [HttpGet("{userId:long}/user-settings")]
    [Authorize(Policy = AuthenticationConstants.UserIdPolicy)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserSettingsDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
    [SwaggerOperation(OperationId = "GetById", Tags = ["UserSettings"])]
    public async Task<IResult> GetById(long userId, CancellationToken cancellationToken = new())
    {
        var entity = await _readUserSettingsService.GetByUserIdAsync(userId);
        return Results.Ok(_mapper.Map<UserSettingsDto>(entity));
    }
}