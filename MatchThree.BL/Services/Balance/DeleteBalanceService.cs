using MatchThree.Domain.Interfaces.Balance;
using MatchThree.Repository.MSSQL;
using MatchThree.Repository.MSSQL.Models;
using MatchThree.Shared.Exceptions;

namespace MatchThree.BL.Services.Balance;

public class DeleteBalanceService (MatchThreeDbContext context)
    : IDeleteBalanceService
{
    private readonly MatchThreeDbContext _context = context;

    public async Task DeleteAsync(long id)
    {
        var dbModel = await _context.Set<BalanceDbModel>().FindAsync(id);
        if (dbModel is null)
            throw new NoDataFoundException();
        
        _context.Set<BalanceDbModel>().Remove(dbModel);
    }
}