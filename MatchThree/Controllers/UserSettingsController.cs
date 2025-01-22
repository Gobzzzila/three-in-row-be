using AutoMapper;
using MatchThree.API.Attributes;
using MatchThree.API.Models.UserSettings;
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
    IUpdateUserSettingsService updateUserSettingsService,
    ITransactionService transactionService,
    IMapper mapper)
{
    private readonly IReadUserSettingsService _readUserSettingsService = readUserSettingsService;
    private readonly IUpdateUserSettingsService _updateUserSettingsService = updateUserSettingsService;
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
    
    /// <summary>
    /// Update culture setting by user identifier 
    /// </summary>
    [HttpPatch("{userId:long}/culture-setting")]
    [Authorize(Policy = AuthenticationConstants.UserIdPolicy)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
    [SwaggerOperation(OperationId = "UpdateCultureSetting", Tags = ["UserSettings"])]
    public async Task<IResult> UpdateCultureSetting([FromMultiSource]UpdateCultureSettingsRequestDto request, 
        CancellationToken cancellationToken = new())
    {
        await _updateUserSettingsService.UpdateCultureAsync(request.UserId, request.Culture);
        await _transactionService.CommitAsync();
        return Results.NoContent();
    }
    
    /// <summary>
    /// Update notifications setting by user identifier
    /// </summary>
    [HttpPatch("{userId:long}/notifications-setting")]
    [Authorize(Policy = AuthenticationConstants.UserIdPolicy)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
    [SwaggerOperation(OperationId = "UpdateNotificationsSetting", Tags = ["UserSettings"])]
    public async Task<IResult> UpdateNotificationsSetting([FromMultiSource]UpdateNotificationsSettingsRequestDto request, 
        CancellationToken cancellationToken = new())
    {
        if (request.Notifications)
            await _updateUserSettingsService.TurnOnNotificationsAsync(request.UserId);
        else
            await _updateUserSettingsService.TurnOffNotificationsAsync(request.UserId);

        await _transactionService.CommitAsync();
        return Results.NoContent();
    }
}