using MatchThree.Domain.Interfaces.Balance;
using MatchThree.Domain.Interfaces.DailyLogin;
using MatchThree.Repository.MSSQL;
using MatchThree.Repository.MSSQL.Models;
using MatchThree.Shared.Exceptions;
using static MatchThree.BL.Configuration.DailyLoginConfiguration;

namespace MatchThree.BL.Services.DailyLogin;

public class ExecuteDailyLoginService(MatchThreeDbContext context,
    IUpdateBalanceService updateBalanceService,
    TimeProvider timeProvider)
    : IExecuteDailyLoginService
{
    private readonly MatchThreeDbContext _context = context;
    private readonly IUpdateBalanceService _updateBalanceService = updateBalanceService;
    private readonly TimeProvider _timeProvider = timeProvider;

    public async Task ExecuteDailyLogin(long userId)
    {
        var dbModel = await _context.Set<DailyLoginDbModel>().FindAsync(userId);
        if (dbModel is null)
            throw new NoDataFoundException();

        var todayDate = _timeProvider.GetUtcNow().Date;
        if (dbModel.LastExecuteDate == todayDate)
            throw new Exception();                  //TODO make specific exception
        
        var isExecutedYesterday = dbModel.LastExecuteDate == todayDate.AddDays(-1);
        var index = isExecutedYesterday ? ShortenIndex(dbModel.StreakCount) : 0;
        await _updateBalanceService.AddBalanceAsync(userId, (uint)DailyRewards[index]);
        
        dbModel.StreakCount = isExecutedYesterday ? dbModel.StreakCount + 1 : 1;
        dbModel.LastExecuteDate = todayDate;
        _context.Set<DailyLoginDbModel>().Update(dbModel);
    }
}