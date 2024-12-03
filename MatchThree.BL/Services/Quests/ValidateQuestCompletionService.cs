using MatchThree.Domain.Interfaces;
using MatchThree.Domain.Interfaces.Energy;
using MatchThree.Domain.Interfaces.Quests;
using MatchThree.Domain.Interfaces.Referral;
using MatchThree.Domain.Interfaces.User;

namespace MatchThree.BL.Services.Quests;

public sealed class ValidateQuestCompletionService(IReadReferralService readReferralService, 
    IReadEnergyService readEnergyService,
    IReadUserService readUserService,
    ITelegramBotService telegramBotService) 
    : IValidateQuestCompletionService
{
    private readonly IReadReferralService _readReferralService = readReferralService;
    private readonly IReadEnergyService _readEnergyService = readEnergyService;
    private readonly IReadUserService _readUserService = readUserService;
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
    
    public async Task<bool> IsEnoughBoughtEnergyDrinksAsync(long userId, int requiredEnergyDrinksAmount)
    {
        var energyEntity = await _readEnergyService.GetByUserIdAsync(userId);
        return energyEntity.PurchasedEnergyDrinkCounter >= requiredEnergyDrinksAmount;
    }
    
    public async Task<bool> IsPremiumUserAsync(long userId)
    {
        var userEntity = await _readUserService.FindByIdAsync(userId);
        return userEntity?.IsPremium ?? false;
    }
}