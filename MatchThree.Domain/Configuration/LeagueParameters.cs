using MatchThree.Shared.Enums;

namespace MatchThree.Domain.Configuration;

public record LeagueParameters
{
    public ulong MinValue { get; init; }
    public ulong MaxValue { get; init; }
    public uint RewardForReferrer { get; init; }
    public LeagueTypes? NextLeague { get; init; }
}