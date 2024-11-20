using MatchThree.Domain.Models;

namespace MatchThree.Domain.Interfaces.Quests;

public interface IReadQuestService
{
    Task<List<QuestEntity>> GetUncompleted(long userId);
    
    Task<List<QuestEntity>> GetCompleted(long userId);
}