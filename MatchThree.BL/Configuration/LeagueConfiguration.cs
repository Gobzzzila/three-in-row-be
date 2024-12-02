using System.Collections.Frozen;
using MatchThree.Domain.Configuration;
using MatchThree.Shared.Constants;
using MatchThree.Shared.Enums;

namespace MatchThree.BL.Configuration;

public static class LeagueConfiguration
{
    private static readonly FrozenDictionary<LeagueTypes, LeagueParameters> LeaguesParams;

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
            ? ReferralConstants.RewardForInvitingPremiumUser
            : ReferralConstants.RewardForInvitingRegularUser;
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
    
    //ctor
    static LeagueConfiguration()
    {
        var dictionary = new Dictionary<LeagueTypes, LeagueParameters>//TODO make foreach and attributes as in fieldconfiguration
        {
            {
                LeagueTypes.Shrimp, new LeagueParameters
                {
                    LeagueFullNameTextKey = TranslationConstants.LeagueShrimpFullNameTextKey,
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
                    LeagueFullNameTextKey = TranslationConstants.LeagueCrabFullNameTextKey,
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
                    LeagueFullNameTextKey = TranslationConstants.LeagueOctopusFullNameTextKey,
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
                    LeagueFullNameTextKey = TranslationConstants.LeagueFishFullNameTextKey,
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
                    LeagueFullNameTextKey = TranslationConstants.LeagueDolphinFullNameTextKey,
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
                    LeagueFullNameTextKey = TranslationConstants.LeagueSharkFullNameTextKey,
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
                    LeagueFullNameTextKey = TranslationConstants.LeagueWhaleFullNameTextKey,
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
                    LeagueFullNameTextKey = TranslationConstants.LeagueHumpbackFullNameTextKey,
                    MinValue = LeagueConstants.WhaleMaxValue,
                    MaxValue = ulong.MaxValue,
                    RewardForReferrer = ReferralConstants.HumpbackLeagueReferrerReward,
                    NextLeague = null,
                    PreviousLeague = LeagueTypes.Whale
                }
            }
        };

        LeaguesParams = dictionary.ToFrozenDictionary();
    }
}