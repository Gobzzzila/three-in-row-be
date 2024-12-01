using MatchThree.Domain.Interfaces.DailyLogin;
using MatchThree.Domain.Models;
using MatchThree.Repository.MSSQL;
using MatchThree.Repository.MSSQL.Models;
using MatchThree.Shared.Exceptions;
using Microsoft.EntityFrameworkCore;
using static MatchThree.BL.Configuration.DailyLoginConfiguration;

namespace MatchThree.BL.Services.DailyLogin;

public class ReadDailyLoginService(MatchThreeDbContext context, 
    TimeProvider timeProvider) 
    : IReadDailyLoginService
{
    private readonly MatchThreeDbContext _context = context;
    private readonly TimeProvider _timeProvider = timeProvider;

    public async Task<DailyLoginEntity> GetDailyLoginInfoByUserIdAsync(long userId)
    {
        var dbModel = await _context.Set<DailyLoginDbModel>().AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == userId);

        if (dbModel is null)
            throw new NoDataFoundException();

        var result = new DailyLoginEntity { Rewards = DailyRewards };

        var todayDate = _timeProvider.GetUtcNow().Date;
        if (dbModel.LastExecuteDate == todayDate)
        {
            result.IsExecutedToday = true;
            result.CurrentIndex = dbModel.StreakCount <= DailyRewards.Count - 1 ? dbModel.StreakCount - 1 : DailyRewards.Count - 1;
            result.StreakCount = dbModel.StreakCount;
            return result;
        }
        
        var isExecutedYesterday = dbModel.LastExecuteDate == todayDate.AddDays(-1);
        result.CurrentIndex = isExecutedYesterday ? ShortenIndex(dbModel.StreakCount) : 0;
        result.StreakCount = isExecutedYesterday ? dbModel.StreakCount : 0;
        result.IsExecutedToday = false;
        return result;
    }
}