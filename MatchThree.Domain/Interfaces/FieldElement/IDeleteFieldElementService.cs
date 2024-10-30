namespace MatchThree.Domain.Interfaces.FieldElement;

public interface IDeleteFieldElementService
{
    Task DeleteByUserIdAsync(long userId);
}