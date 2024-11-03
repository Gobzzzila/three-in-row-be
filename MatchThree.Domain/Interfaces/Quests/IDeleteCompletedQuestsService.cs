namespace MatchThree.Domain.Interfaces.Quests;

public interface IDeleteCompletedQuestsService
{
    Task DeleteByUserIdAsync(long userId);
}