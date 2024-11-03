using AutoMapper;
using MatchThree.API.Models;
using MatchThree.Domain.Interfaces.Quests;
using MatchThree.Shared.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace MatchThree.API.Controllers;

[ApiController]
[Route("api/v1/users")]
public class QuestController(IReadQuestService readQuestService,
    IMapper mapper)
{
    private readonly IReadQuestService _readQuestService = readQuestService;
    private readonly IMapper _mapper = mapper;

    /// <summary>
    /// Get list of uncompleted quests
    /// </summary>
    [HttpGet("{userId:long}/quests/uncompleted")]
    [Authorize(Policy = AuthenticationConstants.UserIdPolicy)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<QuestDto>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
    [SwaggerOperation(OperationId = "GetUncompleted", Tags = ["Quests"])]
    public async Task<IResult> GetUncompletedQuests(long userId, CancellationToken cancellationToken = new())
    {
        var uncompletedQuestEntities = await _readQuestService.GetUncompleted(userId);
        return Results.Ok(_mapper.Map<List<QuestDto>>(uncompletedQuestEntities));
    }
    
    /// <summary>
    /// Get list of completed quests 
    /// </summary>
    [HttpGet("{userId:long}/quests/completed")]
    [Authorize(Policy = AuthenticationConstants.UserIdPolicy)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<QuestDto>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
    [SwaggerOperation(OperationId = "GetCompleted", Tags = ["Quests"])]
    public async Task<IResult> GetCompletedQuests(long userId, CancellationToken cancellationToken = new())
    {
        var completedQuestEntities = await _readQuestService.GetCompleted(userId);
        return Results.Ok(_mapper.Map<List<QuestDto>>(completedQuestEntities));
    }
}