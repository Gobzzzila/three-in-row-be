using MatchThree.Domain.Interfaces.UserSettings;
using MatchThree.Repository.MSSQL;
using MatchThree.Repository.MSSQL.Models;
using MatchThree.Shared.Exceptions;
using MatchThree.Shared.Extensions;

namespace MatchThree.BL.Services.UserSettings;

public class UpdateUserSettingsService(MatchThreeDbContext context) 
    : IUpdateUserSettingsService
{
    private readonly MatchThreeDbContext _context = context;

    public async Task UpdateCultureAsync(long id, string newCulture)
    {
        var dbModel = await _context.Set<UserSettingsDbModel>().FindAsync(id);
        if (dbModel is null)
            throw new NoDataFoundException();

        dbModel.Culture = newCulture.ReadableLanguageToCultureTypes();
        
        _context.Set<UserSettingsDbModel>().Update(dbModel);
    }

    public async Task TurnOnNotificationsAsync(long id)
    {
        var dbModel = await _context.Set<UserSettingsDbModel>().FindAsync(id);
        if (dbModel is null)
            throw new NoDataFoundException();
        
        dbModel.Notifications = true;
        
        _context.Set<UserSettingsDbModel>().Update(dbModel);
    }
    
    public async Task TurnOffNotificationsAsync(long id)
    {
        var dbModel = await _context.Set<UserSettingsDbModel>().FindAsync(id);
        if (dbModel is null)
            throw new NoDataFoundException();
        
        dbModel.Notifications = false;
        
        _context.Set<UserSettingsDbModel>().Update(dbModel);
    }
}