using MatchThree.Shared.Enums;

namespace MatchThree.API.Models.Referrals;

public class ReferralDto
{
    public long Id { get; init; }
    public string FirstName { get; init; } = string.Empty;
    public bool WasPremium { get; init; }
    public LeagueTypes League { get; init; }
    public uint Brought { get; init; }
}