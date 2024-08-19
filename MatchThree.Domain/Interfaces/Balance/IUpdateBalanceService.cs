namespace MatchThree.Domain.Interfaces.Balance;

public interface IUpdateBalanceService
{
    Task AddBalanceAsync(long id, uint amount);
}