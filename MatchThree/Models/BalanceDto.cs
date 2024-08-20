using MatchThree.Shared.Enums;

namespace MatchThree.API.Models;

public class BalanceDto
{
    public long Id { get; set; }
    public uint Balance { get; set; }
    public LeagueTypes League { get; set; }
    public int TopSpot { get; set; }
}