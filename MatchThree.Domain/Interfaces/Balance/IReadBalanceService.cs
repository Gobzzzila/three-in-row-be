using MatchThree.Domain.Models;

namespace MatchThree.Domain.Interfaces.Balance;

public interface IReadBalanceService
{
    Task<BalanceEntity> GetByIdAsync(long id);
}