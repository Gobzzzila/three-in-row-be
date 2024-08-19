using MatchThree.Domain.Interfaces.Balance;
using MatchThree.Repository.MSSQL;
using MatchThree.Repository.MSSQL.Models;

namespace MatchThree.BL.Services.Balance;

public class DeleteBalanceService (MatchThreeDbContext context)
    : IDeleteBalanceService
{
    public async Task DeleteAsync(long id)
    {
        var dbModel = await context.Set<BalanceDbModel>().FindAsync(id);
        context.Set<BalanceDbModel>().Remove(dbModel!);
    }
}