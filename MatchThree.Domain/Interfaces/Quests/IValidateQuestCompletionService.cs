namespace MatchThree.Domain.Interfaces.Quests;

public interface IValidateQuestCompletionService
{
    Task<bool> IsEnoughReferralsAsync(long userId, int requiredReferralsAmount);
    
    Task<bool> IsSubscribedToChannelAsync(long userId, params long[] chatIds);
    
    Task<bool> IsEnoughBoughtEnergyDrinksAsync(long userId, int requiredEnergyDrinksAmount);
    
    Task<bool> IsPremiumUserAsync(long userId);
}