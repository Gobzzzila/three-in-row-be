namespace MatchThree.Shared.Constants;

public static class ReferralConstants
{
    public const uint RewardForInvitingRegularUser = 500;
    public const uint RewardForInvitingPremiumUser = 2000;

    public const uint CrabLeagueReferrerReward = 1_000;
    public const uint OctopusLeagueReferrerReward = 3_000;
    public const uint FishLeagueReferrerReward = 5_000;
    public const uint DolphinLeagueReferrerReward = 15_000;
    public const uint SharkLeagueReferrerReward = 30_000;
    public const uint WhaleLeagueReferrerReward = 54_000;
    public const uint HumpbackLeagueReferrerReward = 120_000;

    public const uint AmountOfRewardsForIncreasingLevels = CrabLeagueReferrerReward +
                                                           OctopusLeagueReferrerReward +
                                                           FishLeagueReferrerReward +
                                                           DolphinLeagueReferrerReward +
                                                           SharkLeagueReferrerReward +
                                                           WhaleLeagueReferrerReward +
                                                           HumpbackLeagueReferrerReward;
}