using MatchThree.Domain.Interfaces.Notifications;
using MatchThree.Domain.Interfaces.UserSettings;
using MatchThree.Repository.MSSQL;
using MatchThree.Repository.MSSQL.Models;
using MatchThree.Shared.Exceptions;
using MatchThree.Shared.Extensions;

namespace MatchThree.BL.Services.UserSettings;

public class UpdateUserSettingsService(MatchThreeDbContext context,
    IUpdateNotificationsService updateNotificationsService) 
    : IUpdateUserSettingsService
{
    private readonly MatchThreeDbContext _context = context;
    private readonly IUpdateNotificationsService _updateNotificationsService = updateNotificationsService;

    public async Task UpdateCultureAsync(long userId, string newCulture)
    {
        var dbModel = await _context.Set<UserSettingsDbModel>().FindAsync(userId);
        if (dbModel is null)
            throw new NoDataFoundException();

        dbModel.Culture = newCulture.ReadableLanguageToCultureTypes();
        _context.Set<UserSettingsDbModel>().Update(dbModel);
    }

    public async Task TurnOnNotificationsAsync(long userId)
    {
        var dbModel = await _context.Set<UserSettingsDbModel>().FindAsync(userId);
        if (dbModel is null)
            throw new NoDataFoundException();

        await _updateNotificationsService.ActualizeEnergyNotificationTimeAsync(userId);
        
        dbModel.Notifications = true;
        _context.Set<UserSettingsDbModel>().Update(dbModel);
    }
    
    public async Task TurnOffNotificationsAsync(long userId)
    {
        var dbModel = await _context.Set<UserSettingsDbModel>().FindAsync(userId);
        if (dbModel is null)
            throw new NoDataFoundException();
        
        dbModel.Notifications = false;
        _context.Set<UserSettingsDbModel>().Update(dbModel);
    }
}