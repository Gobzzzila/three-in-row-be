using MatchThree.Shared.Enums;

namespace MatchThree.Domain.Configuration;

public record LeagueParameters
{
    public ulong MinValue { get; init; }
    public ulong MaxValue { get; init; }
    public uint RewardForReferrer { get; init; }
    public string LeagueFullNameTextKey { get; init; } = string.Empty;
    public LeagueTypes? NextLeague { get; init; }
    public LeagueTypes? PreviousLeague { get; init; }
}