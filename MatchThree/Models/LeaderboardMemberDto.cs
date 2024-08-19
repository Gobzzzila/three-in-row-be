using MatchThree.Shared.Enums;

namespace MatchThree.API.Models;

public class LeaderboardMemberDto
{
    public long Id { get; set; }
    public LeagueTypes League { get; set; }
    public ushort TopSpot { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public ulong OverallBalance { get; set; }
}