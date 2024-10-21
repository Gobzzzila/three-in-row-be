using MatchThree.Domain.Models;

namespace MatchThree.Domain.Interfaces.FieldElements;

public interface IReadFieldElementsService
{
    Task<FieldEntity> GetByUserIdAsync(long userId);
}