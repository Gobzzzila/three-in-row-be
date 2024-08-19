namespace MatchThree.Domain.Interfaces.Balance;

public interface ICreateBalanceService
{
    Task CreateAsync(long userId, uint initialBalance);
}