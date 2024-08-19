namespace MatchThree.Domain.Interfaces.Balance;

public interface IDeleteBalanceService
{
    Task DeleteAsync(long id);
}