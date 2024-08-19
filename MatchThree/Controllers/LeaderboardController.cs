using AutoMapper;
using MatchThree.API.Models;
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
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IReadOnlyCollection<LeaderboardMemberDto>))]
    public async Task<IResult> GetLeagueMembers(int league, CancellationToken cancellationToken = new())
    {
        var entities = 
            await readLeaderboardMemberService.GetLeaderboardByLeagueAsync((LeagueTypes)league);
        return Results.Ok(mapper.Map<IReadOnlyCollection<LeaderboardMemberDto>>(entities));
    }
}