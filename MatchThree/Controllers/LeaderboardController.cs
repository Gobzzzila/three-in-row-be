using AutoMapper;
using MatchThree.API.Models.Leaderboard;
using MatchThree.Domain.Interfaces.LeaderboardMember;
using MatchThree.Shared.Enums;
using Microsoft.AspNetCore.Mvc;

namespace MatchThree.API.Controllers;

[ApiController]
[Route("api/v1/leaderboards")]
public class LeaderboardController(IReadLeaderboardMemberService readLeaderboardMemberService,
    IMapper mapper)
{
    /// <summary>
    /// Get leaderboard by league 
    /// </summary>
    [HttpGet("leagues/{league:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LeaderboardDto))]
    public async Task<IResult> GetLeaderboard(int league, CancellationToken cancellationToken = new())
    {
        var entity = await readLeaderboardMemberService.GetLeaderboardByLeagueAsync((LeagueTypes)league);
        return Results.Ok(mapper.Map<LeaderboardDto>(entity));
    }
}