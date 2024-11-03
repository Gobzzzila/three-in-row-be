namespace MatchThree.Domain.Interfaces.Quests;

public interface ICreateCompletedQuestsService
{
    void Create(long userId);
}