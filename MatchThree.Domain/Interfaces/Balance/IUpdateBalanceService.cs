namespace MatchThree.Domain.Interfaces.Balance;

public interface IUpdateBalanceService
{
    Task SpentBalanceAsync(long id, uint amount);
    Task AddBalanceAsync(long id, uint amount);
}