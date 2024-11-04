namespace MatchThree.Domain.Interfaces.Quests;

public interface IValidateQuestCompletionService
{
    Task<bool> IsEnoughReferralsAsync(long userId, int requiredReferralsAmount);
}