namespace MatchThree.Domain.Interfaces.Field;

public interface IUpdateFieldService
{
    Task UpgradeFieldAsync(long userId);
    
    Task UpdateFieldAsync(long userId, int[][] field);
}