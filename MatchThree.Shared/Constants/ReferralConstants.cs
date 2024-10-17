namespace MatchThree.Shared.Constants;

public static class ReferralConstants
{
    public const uint RewardForInvitingRegularUser = 500;
    public const uint RewardForInvitingPremiumUser = 2000;

    public const uint CrabLeagueReferrerReward = 1_000;
    public const uint OctopusLeagueReferrerReward = 1_000;
    public const uint FishLeagueReferrerReward = 1_000;
    public const uint DolphinLeagueReferrerReward = 1_000;
    public const uint SharkLeagueReferrerReward = 1_000;
    public const uint WhaleLeagueReferrerReward = 1_000;
    public const uint HumpbackLeagueReferrerReward = 1_000;
    
    public static readonly uint AmountOfRewardsForIncreasingLevels = CrabLeagueReferrerReward +
                                                                     OctopusLeagueReferrerReward +
                                                                     FishLeagueReferrerReward +
                                                                     DolphinLeagueReferrerReward +
                                                                     SharkLeagueReferrerReward +
                                                                     WhaleLeagueReferrerReward +
                                                                     HumpbackLeagueReferrerReward;
}