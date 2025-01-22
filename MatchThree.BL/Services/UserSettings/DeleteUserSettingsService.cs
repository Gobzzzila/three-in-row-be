using MatchThree.Domain.Interfaces.UserSettings;
using MatchThree.Repository.MSSQL;
using MatchThree.Repository.MSSQL.Models;
using MatchThree.Shared.Exceptions;

namespace MatchThree.BL.Services.UserSettings;

public class DeleteUserSettingsService(MatchThreeDbContext context) 
    : IDeleteUserSettingsService
{
    private readonly MatchThreeDbContext _context = context;

    public async Task DeleteAsync(long id)
    {
        var dbModel = await _context.Set<UserSettingsDbModel>().FindAsync(id);
        if (dbModel is null)
            throw new NoDataFoundException();
        
        _context.Set<UserSettingsDbModel>().Remove(dbModel);
    }
}