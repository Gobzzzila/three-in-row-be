using MatchThree.Domain.Interfaces;
using MatchThree.Domain.Interfaces.Quests;
using MatchThree.Domain.Interfaces.Referral;

namespace MatchThree.BL.Services.Quests;

public sealed class ValidateQuestCompletionService(IReadReferralService readReferralService, 
    ITelegramBotService telegramBotService) 
    : IValidateQuestCompletionService
{
    private readonly IReadReferralService _readReferralService = readReferralService;
    private readonly ITelegramBotService _telegramBotService = telegramBotService;

    public async Task<bool> IsEnoughReferralsAsync(long userId, int requiredReferralsAmount)
    {
        var referralsAmount = await _readReferralService.GetReferralAmountByReferrerIdAsync(userId);
        return referralsAmount >= requiredReferralsAmount;
    }

    public async Task<bool> IsSubscribedToChannelAsync(long userId, params long[] chatIds)
    {
        var isSubscribed = await _telegramBotService.IsSubscribedToChannelAsync(userId, chatIds);
        return isSubscribed;
    }
}