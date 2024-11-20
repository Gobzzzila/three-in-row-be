using MatchThree.Domain.Models;

namespace MatchThree.Domain.Interfaces.FieldElement;

public interface IReadFieldElementService
{
    Task<List<FieldElementEntity>> GetByUserIdAsync(long userId);
}