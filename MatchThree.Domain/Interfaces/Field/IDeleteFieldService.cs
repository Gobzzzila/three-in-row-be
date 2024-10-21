namespace MatchThree.Domain.Interfaces.Field;

public interface IDeleteFieldService
{
    Task DeleteAsync(long id);
}