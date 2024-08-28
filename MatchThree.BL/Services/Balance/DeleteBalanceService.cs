using MatchThree.Domain.Interfaces.Balance;
using MatchThree.Repository.MSSQL;
using MatchThree.Repository.MSSQL.Models;

namespace MatchThree.BL.Services.Balance;

public class DeleteBalanceService (MatchThreeDbContext context)
    : IDeleteBalanceService
{
    private readonly MatchThreeDbContext _context = context;

    public async Task DeleteAsync(long id)
    {
        var dbModel = await _context.Set<BalanceDbModel>().FindAsync(id);
        _context.Set<BalanceDbModel>().Remove(dbModel!);
    }
}