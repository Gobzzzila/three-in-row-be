using MatchThree.Domain.Interfaces.Quests;
using MatchThree.Domain.Interfaces.Referral;

namespace MatchThree.BL.Services.Quests;

public sealed class QuestCompletionService(IReadReferralService readReferralService) 
    : IQuestCompletionService
{
    private readonly IReadReferralService _readReferralService = readReferralService;

    public async Task<bool> IsEnoughReferralsAsync(long userId, int requiredReferralsAmount)
    {
        var referralsAmount = await _readReferralService.GetReferralAmountByReferrerIdAsync(userId);
        return referralsAmount >= requiredReferralsAmount;
    }
}