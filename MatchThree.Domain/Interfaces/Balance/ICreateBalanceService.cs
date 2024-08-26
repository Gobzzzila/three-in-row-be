namespace MatchThree.Domain.Interfaces.Balance;

public interface ICreateBalanceService
{
    void Create(long userId, uint initialBalance);
}