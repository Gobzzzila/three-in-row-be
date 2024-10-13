namespace MatchThree.Domain.Interfaces.FieldElements;

public interface IDeleteFieldElementsService
{
    Task DeleteAsync(long id);
}