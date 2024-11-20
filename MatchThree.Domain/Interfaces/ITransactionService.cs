namespace MatchThree.Domain.Interfaces;

public interface ITransactionService
{
    Task CommitAsync();
    
    void CleanChangeTracker();
}