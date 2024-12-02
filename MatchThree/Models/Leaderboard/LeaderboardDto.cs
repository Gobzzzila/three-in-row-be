using MatchThree.Shared.Enums;

namespace MatchThree.API.Models.Leaderboard;

public class LeaderboardDto
{
    public LeagueTypes League { get; init; }
    public string LeagueFullName { get; init; } = string.Empty;
    public ulong MinValue { get; init; }
    public ulong MaxValue { get; init; }
    public LeagueTypes? NextLeague { get; init; }
    public LeagueTypes? PreviousLeague { get; init; }
    public List<LeaderboardMemberDto> Members { get; init; } = [];
}