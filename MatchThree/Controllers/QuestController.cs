using AutoMapper;
using MatchThree.API.Attributes;
using MatchThree.API.Models;
using MatchThree.Domain.Interfaces;
using MatchThree.Domain.Interfaces.Quests;
using MatchThree.Shared.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace MatchThree.API.Controllers;

[ApiController]
[Route("api/v1/users")]
public class QuestController(IReadQuestService readQuestService,
    ICompleteQuestService completeQuestService,
    ITransactionService transactionService,
    IMapper mapper)
{
    private readonly IReadQuestService _readQuestService = readQuestService;
    private readonly ICompleteQuestService _completeQuestService = completeQuestService;
    private readonly ITransactionService _transactionService = transactionService;
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
    
    /// <summary>
    /// Complete quest
    /// </summary>
    [HttpPost("{userId:long}/quests/{questId:guid}")]
    [Authorize(Policy = AuthenticationConstants.UserIdPolicy)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    [SwaggerOperation(OperationId = "CompleteQuest", Tags = ["Quests"])]
    public async Task<IResult> CompleteQuest([FromMultiSource] CompleteQuestRequestDto request, 
        CancellationToken cancellationToken = new())
    {
        await _completeQuestService.CompleteQuest(request.UserId, request.QuestId, request.SecretCode);
        await _transactionService.CommitAsync();
        return Results.NoContent();
    }
}