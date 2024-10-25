namespace MatchThree.Domain.Interfaces.Field;

public interface IMoveService
{
    Task MakeMoveAsync(long userId, uint reward, int[][] field, string hash);
}