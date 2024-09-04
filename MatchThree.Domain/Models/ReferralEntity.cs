using MatchThree.Shared.Enums;

namespace MatchThree.Domain.Models;

public class ReferralEntity
{
    public long Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public bool WasPremium { get; set; }
    public LeagueTypes League { get; set; }
    public ulong OverallBalance { get; set; }
    public uint Brought { get; set; }
}