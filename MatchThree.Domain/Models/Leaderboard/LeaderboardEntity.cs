using MatchThree.Shared.Enums;

namespace MatchThree.Domain.Models.Leaderboard;

public class LeaderboardEntity
{
    public LeagueTypes League { get; set; }
    public string LeagueFullNameTextKey { get; set; } = string.Empty;
    public ulong MinValue { get; set; }
    public ulong MaxValue { get; set; }
    public LeagueTypes? NextLeague { get; set; }
    public LeagueTypes? PreviousLeague { get; set; }
    public List<LeaderboardMemberEntity> Members { get; set; } = [];
}