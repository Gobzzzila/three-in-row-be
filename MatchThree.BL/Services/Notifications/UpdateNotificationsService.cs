using MatchThree.Domain.Interfaces.Notifications;
using MatchThree.Repository.MSSQL;
using MatchThree.Repository.MSSQL.Models;
using MatchThree.Shared.Exceptions;

namespace MatchThree.BL.Services.Notifications;

public class UpdateNotificationsService(MatchThreeDbContext context, 
    TimeProvider timeProvider) 
    : IUpdateNotificationsService
{
    private readonly MatchThreeDbContext _context = context;
    private readonly TimeProvider _timeProvider = timeProvider;

    public async ValueTask ResetEnergyNotificationTimeAsync(long userId)
    {
        var dbModel = await _context.Set<NotificationsDbModel>().FindAsync(userId);
        if (dbModel is null)
            throw new NoDataFoundException();

        dbModel.EnergyNotification = null;
        _context.Set<NotificationsDbModel>().Update(dbModel);
    }
    
    public async Task ActualizeEnergyNotificationTimeAsync(long userId)
    {
        var dbModel = await _context.Set<NotificationsDbModel>().FindAsync(userId);
        if (dbModel is null)
            throw new NoDataFoundException();
     
        var now = _timeProvider.GetUtcNow().DateTime;
        if (dbModel.EnergyNotification is null || dbModel.EnergyNotification > now)
            return;

        dbModel.EnergyNotification = null;
        _context.Set<NotificationsDbModel>().Update(dbModel);
    }
    
    public async Task SetEnergyNotificationTimeAsync(long userId, DateTime? notificationTime)
    {
        var dbModel = await _context.Set<NotificationsDbModel>().FindAsync(userId);
        if (dbModel is null)
            throw new NoDataFoundException();

        dbModel.EnergyNotification = notificationTime;
        _context.Set<NotificationsDbModel>().Update(dbModel);
    }
}