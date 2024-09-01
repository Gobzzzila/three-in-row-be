namespace MatchThree.API.Models.Leaderboard;

public class LeaderboardMemberDto
{
    public int TopSpot { get; init; }
    public string FirstName { get; init; } = string.Empty;
    public ulong OverallBalance { get; init; }
}