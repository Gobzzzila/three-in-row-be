using MatchThree.Shared.Enums;

namespace MatchThree.API.Models;

public class BalanceDto
{
    public long Id { get; init; }
    public uint Balance { get; init; }
    public ulong OverallBalance { get; set; }
    public LeagueTypes League { get; init; }
    public int TopSpot { get; init; }
}