using MatchThree.Shared.Enums;

namespace MatchThree.API.Models.Referrals;

public class ReferrerRewardPerLeagueDto
{
    public LeagueTypes League { get; init; }
    public string LeagueName { get; init; } = string.Empty;
    public uint Reward { get; init; }
}