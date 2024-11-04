using MatchThree.Domain.Interfaces.DailyLogin;
using MatchThree.Repository.MSSQL;
using MatchThree.Repository.MSSQL.Models;
using MatchThree.Shared.Exceptions;

namespace MatchThree.BL.Services.DailyLogin;

public class DeleteDailyLoginService(MatchThreeDbContext context)
    : IDeleteDailyLoginService
{
    private readonly MatchThreeDbContext _context = context;

    public async Task DeleteAsync(long id)
    {
        var dbModel = await _context.Set<DailyLoginDbModel>().FindAsync(id);
        if (dbModel is null)
            throw new NoDataFoundException();

        _context.Set<DailyLoginDbModel>().Remove(dbModel);
    }
}