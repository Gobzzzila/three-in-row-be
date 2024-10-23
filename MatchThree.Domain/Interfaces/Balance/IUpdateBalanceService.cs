namespace MatchThree.Domain.Interfaces.Balance;

public interface IUpdateBalanceService
{
    Task SpendBalanceAsync(long id, uint amount);
    Task AddBalanceAsync(long id, uint amount);
}