namespace MatchThree.Domain.Interfaces;

public interface ITransactionService
{
    Task Commit();
    void CleanChangeTracker();
}