using MatchThree.Shared.Enums;

namespace MatchThree.API.Models.Leaderboard;

public class LeaderboardDto
{
    public string LeagueName { get; init; } = string.Empty;
    public LeagueTypes? NextLeague { get; init; }
    public LeagueTypes? PreviousLeague { get; init; }
    public List<LeaderboardMemberDto> Members { get; init; } = [];
}