using MatchThree.Domain.Models;

namespace MatchThree.Domain.Interfaces.Field;

public interface IReadFieldService
{
    Task<FieldEntity> GetByUserIdAsync(long userId);
}