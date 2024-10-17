namespace MatchThree.API.Models.Referrals;

public class ReferrerRewardDto
{
    public uint RewardForInvitingRegularUser { get; init; }
    public uint RewardForInvitingPremiumUser { get; init; }
    public uint AmountOfRewardsForIncreasingLevels { get; init; }
    public List<ReferrerRewardPerLeagueDto> RewardPerLeague { get; init; } = [];
}