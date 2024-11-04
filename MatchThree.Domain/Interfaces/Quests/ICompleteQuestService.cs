namespace MatchThree.Domain.Interfaces.Quests;

public interface ICompleteQuestService
{
    Task CompleteQuest(long userId, Guid questId, string? secretCode);
}