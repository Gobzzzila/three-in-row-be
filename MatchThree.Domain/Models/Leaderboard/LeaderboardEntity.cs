using MatchThree.Shared.Enums;

namespace MatchThree.Domain.Models.Leaderboard;

public class LeaderboardEntity
{
    public LeagueTypes League { get; init; }
    public LeagueTypes? NextLeague { get; init; }
    public LeagueTypes? PreviousLeague { get; init; }
    public List<LeaderboardMemberEntity> Members { get; init; } = [];
}