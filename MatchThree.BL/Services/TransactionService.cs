using MatchThree.Domain.Interfaces;
using MatchThree.Repository.MSSQL;
using Microsoft.EntityFrameworkCore;

namespace MatchThree.BL.Services;

public class TransactionService : ITransactionService
{
    private MatchThreeDbContext Context { get; }

    public TransactionService(MatchThreeDbContext context)
    {
        Context = context;
    }

    public async Task Commit() => await Context.Database
        .CreateExecutionStrategy()
        .ExecuteAsync(RunTransaction);

    public void CleanChangeTracker()
    {
        Context.ChangeTracker.Clear();  
    } 

    private async Task RunTransaction()
    {
        await using var transaction = await Context.Database.BeginTransactionAsync();
        try
        {
            Context.ChangeTracker.DetectChanges();
            await Context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (DbUpdateException)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}