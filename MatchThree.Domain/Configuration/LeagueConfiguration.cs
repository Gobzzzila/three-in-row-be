using MatchThree.Shared.Constants;
using MatchThree.Shared.Enums;

namespace MatchThree.Domain.Configuration;

public static class LeagueConfiguration
{
    private static readonly Dictionary<LeagueTypes, LeagueParameters> LeaguesParams;

    static LeagueConfiguration()
    {
        LeaguesParams = new Dictionary<LeagueTypes, LeagueParameters>
        {
            {
                LeagueTypes.Shrimp, new LeagueParameters
                {
                    MinValue = 0,
                    MaxValue = LeagueConstants.ShrimpMaxValue,
                    RewardForReferrer = 0,
                    NextLeague = LeagueTypes.Crab
                }
            },
            {
                LeagueTypes.Crab, new LeagueParameters
                {
                    MinValue = LeagueConstants.ShrimpMaxValue,
                    MaxValue = LeagueConstants.CrabMaxValue,
                    RewardForReferrer = ReferralConstants.CrabLeagueReferrerReward,
                    NextLeague = LeagueTypes.Octopus
                }
            },
            {
                LeagueTypes.Octopus, new LeagueParameters
                {
                    MinValue = LeagueConstants.CrabMaxValue,
                    MaxValue = LeagueConstants.OctopusMaxValue,
                    RewardForReferrer = ReferralConstants.OctopusLeagueReferrerReward,
                    NextLeague = LeagueTypes.Fish
                }
            },
            {
                LeagueTypes.Fish, new LeagueParameters
                {
                    MinValue = LeagueConstants.OctopusMaxValue,
                    MaxValue = LeagueConstants.FishMaxValue,
                    RewardForReferrer = ReferralConstants.FishLeagueReferrerReward,
                    NextLeague = LeagueTypes.Dolphin
                }
            },
            {
                LeagueTypes.Dolphin, new LeagueParameters
                {
                    MinValue = LeagueConstants.FishMaxValue,
                    MaxValue = LeagueConstants.DolphinMaxValue,
                    RewardForReferrer = ReferralConstants.DolphinLeagueReferrerReward,
                    NextLeague = LeagueTypes.Shark
                }
            },
            {
                LeagueTypes.Shark, new LeagueParameters
                {
                    MinValue = LeagueConstants.DolphinMaxValue,
                    MaxValue = LeagueConstants.SharkMaxValue,
                    RewardForReferrer = ReferralConstants.SharkLeagueReferrerReward,
                    NextLeague = LeagueTypes.Whale
                }
            },
            {
                LeagueTypes.Whale, new LeagueParameters
                {
                    MinValue = LeagueConstants.SharkMaxValue,
                    MaxValue = LeagueConstants.WhaleMaxValue,
                    RewardForReferrer = ReferralConstants.WhaleLeagueReferrerReward,
                    NextLeague = LeagueTypes.Humpback
                }
            },
            {
                LeagueTypes.Humpback, new LeagueParameters
                {
                    MinValue = LeagueConstants.WhaleMaxValue,
                    MaxValue = ulong.MaxValue,
                    RewardForReferrer = ReferralConstants.HumpbackLeagueReferrerReward
                }
            }
        };
    }

    public static LeagueTypes CalculateLeague(ulong overallBalance)
    {
        return (from leagueRange in LeaguesParams
            where overallBalance >= leagueRange.Value.MinValue && overallBalance < leagueRange.Value.MaxValue
            select leagueRange.Key).FirstOrDefault();
    }
    
    public static (bool isUpped, uint rewardForReferrer) IsLeagueUpped (ulong overallBalance, uint amountToAdd)
    {
        var oldLeague = CalculateLeague(overallBalance);
        var newLeague = CalculateLeague(overallBalance + amountToAdd);

        return (oldLeague < newLeague, LeaguesParams[newLeague].RewardForReferrer);
    }
    
    public static LeagueParameters GetParamsByType(LeagueTypes league)
    {
        return LeaguesParams[league];
    }
    
    public static LeagueTypes GetNextLeagueLooped(LeagueTypes league)
    {
        return LeaguesParams.TryGetValue(league, out var leagueParams)
            ? leagueParams.NextLeague ?? LeagueTypes.Shrimp
            : LeagueTypes.Shrimp;
    }
}