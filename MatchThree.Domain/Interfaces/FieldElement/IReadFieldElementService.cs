using MatchThree.Domain.Models;
using MatchThree.Repository.MSSQL.Models;

namespace MatchThree.Domain.Interfaces.FieldElement;

public interface IReadFieldElementService
{
    Task<List<FieldElementEntity>> GetByUserIdAsync(long userId);
}