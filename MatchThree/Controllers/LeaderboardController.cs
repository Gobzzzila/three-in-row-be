using AutoMapper;
using MatchThree.API.Models.Leaderboard;
using MatchThree.Domain.Interfaces.LeaderboardMember;
using MatchThree.Shared.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace MatchThree.API.Controllers;

[ApiController]
[Route("api/v1/leaderboards")]
public class LeaderboardController(IReadLeaderboardMemberService readLeaderboardMemberService,
    IMapper mapper)
{
    private readonly IReadLeaderboardMemberService _readLeaderboardMemberService = readLeaderboardMemberService;
    private readonly IMapper _mapper = mapper;
    
    /// <summary>
    /// Get leaderboard by league 
    /// </summary>
    [HttpGet("leagues/{league:int}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LeaderboardDto))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
    [SwaggerOperation(OperationId = "GetLeaderboardByLeagueId", Tags = ["Leaderboard"])]
    public async Task<IResult> GetLeaderboard(int league, CancellationToken cancellationToken = new())
    {
        var entity = await _readLeaderboardMemberService.GetLeaderboardByLeagueAsync((LeagueTypes)league);
        return Results.Ok(_mapper.Map<LeaderboardDto>(entity));
    }
}