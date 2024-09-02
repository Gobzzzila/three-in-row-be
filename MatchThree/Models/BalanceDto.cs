using MatchThree.Shared.Enums;

namespace MatchThree.API.Models;

public class BalanceDto
{
    public long Id { get; init; }
    public uint Balance { get; init; }
    public LeagueTypes League { get; init; }
    public int TopSpot { get; init; }
}