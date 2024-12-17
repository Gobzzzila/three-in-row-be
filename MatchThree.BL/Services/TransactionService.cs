using MatchThree.Domain.Interfaces;
using MatchThree.Repository.MSSQL;
using Microsoft.EntityFrameworkCore;

namespace MatchThree.BL.Services;

public class TransactionService(MatchThreeDbContext context) : ITransactionService
{
    private readonly MatchThreeDbContext _context = context;

    public async Task CommitAsync() => await _context.Database
        .CreateExecutionStrategy()
        .ExecuteAsync(RunTransaction);

    public void CleanChangeTracker()
    {
        _context.ChangeTracker.Clear();  
    } 

    private async Task RunTransaction()
    {
        await using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            _context.ChangeTracker.DetectChanges(); // Better safe than sorry c:
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (DbUpdateException)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}