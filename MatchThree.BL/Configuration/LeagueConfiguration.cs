using MatchThree.Domain.Configuration;
using MatchThree.Shared.Constants;
using MatchThree.Shared.Enums;

namespace MatchThree.BL.Configuration;

public static class LeagueConfiguration
{
    private static readonly Dictionary<LeagueTypes, LeagueParameters> LeaguesParams;

    public static LeagueTypes CalculateLeague(ulong overallBalance)
    {
        return (from leagueRange in LeaguesParams
            where overallBalance >= leagueRange.Value.MinValue && overallBalance < leagueRange.Value.MaxValue
            select leagueRange.Key).FirstOrDefault();
    }
    
    public static uint CalculateReferralProfit(LeagueTypes league, bool wasPremium)
    {
        uint referralProfit = 0;
        foreach (var leaguesParam in LeaguesParams)
        {
            referralProfit += leaguesParam.Value.RewardForReferrer;
            
            if (leaguesParam.Key == league)
                break;
        }
        referralProfit += wasPremium
            ? ReferralConstants.RewardPremiumUserForBeingInvited
            : ReferralConstants.RewardRegularUserForBeingInvited;
        return referralProfit;
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
    
    //ctor
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
                    NextLeague = LeagueTypes.Crab,
                    PreviousLeague = null
                }
            },
            {
                LeagueTypes.Crab, new LeagueParameters
                {
                    MinValue = LeagueConstants.ShrimpMaxValue,
                    MaxValue = LeagueConstants.CrabMaxValue,
                    RewardForReferrer = ReferralConstants.CrabLeagueReferrerReward,
                    NextLeague = LeagueTypes.Octopus,
                    PreviousLeague = LeagueTypes.Shrimp
                }
            },
            {
                LeagueTypes.Octopus, new LeagueParameters
                {
                    MinValue = LeagueConstants.CrabMaxValue,
                    MaxValue = LeagueConstants.OctopusMaxValue,
                    RewardForReferrer = ReferralConstants.OctopusLeagueReferrerReward,
                    NextLeague = LeagueTypes.Fish,
                    PreviousLeague = LeagueTypes.Crab
                }
            },
            {
                LeagueTypes.Fish, new LeagueParameters
                {
                    MinValue = LeagueConstants.OctopusMaxValue,
                    MaxValue = LeagueConstants.FishMaxValue,
                    RewardForReferrer = ReferralConstants.FishLeagueReferrerReward,
                    NextLeague = LeagueTypes.Dolphin,
                    PreviousLeague = LeagueTypes.Octopus
                }
            },
            {
                LeagueTypes.Dolphin, new LeagueParameters
                {
                    MinValue = LeagueConstants.FishMaxValue,
                    MaxValue = LeagueConstants.DolphinMaxValue,
                    RewardForReferrer = ReferralConstants.DolphinLeagueReferrerReward,
                    NextLeague = LeagueTypes.Shark,
                    PreviousLeague = LeagueTypes.Fish
                }
            },
            {
                LeagueTypes.Shark, new LeagueParameters
                {
                    MinValue = LeagueConstants.DolphinMaxValue,
                    MaxValue = LeagueConstants.SharkMaxValue,
                    RewardForReferrer = ReferralConstants.SharkLeagueReferrerReward,
                    NextLeague = LeagueTypes.Whale,
                    PreviousLeague = LeagueTypes.Dolphin
                }
            },
            {
                LeagueTypes.Whale, new LeagueParameters
                {
                    MinValue = LeagueConstants.SharkMaxValue,
                    MaxValue = LeagueConstants.WhaleMaxValue,
                    RewardForReferrer = ReferralConstants.WhaleLeagueReferrerReward,
                    NextLeague = LeagueTypes.Humpback,
                    PreviousLeague = LeagueTypes.Shark
                }
            },
            {
                LeagueTypes.Humpback, new LeagueParameters
                {
                    MinValue = LeagueConstants.WhaleMaxValue,
                    MaxValue = ulong.MaxValue,
                    RewardForReferrer = ReferralConstants.HumpbackLeagueReferrerReward,
                    NextLeague = null,
                    PreviousLeague = LeagueTypes.Whale
                }
            }
        };
    }
}