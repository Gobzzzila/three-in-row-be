namespace MatchThree.Domain.Interfaces.Quests;

public interface IQuestCompletionService
{
    Task<bool> IsEnoughReferralsAsync(long userId, int requiredReferralsAmount);
}