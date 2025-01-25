using MatchThree.Domain.Interfaces.Notifications;
using MatchThree.Repository.MSSQL;
using MatchThree.Repository.MSSQL.Models;
using MatchThree.Shared.Exceptions;

namespace MatchThree.BL.Services.Notifications;

public class DeleteNotificationsService(MatchThreeDbContext context) 
    : IDeleteNotificationsService
{
    private readonly MatchThreeDbContext _context = context;

    public async Task DeleteAsync(long id)
    {
        var dbModel = await _context.Set<NotificationsDbModel>().FindAsync(id);
        if (dbModel is null)
            throw new NoDataFoundException();
        
        _context.Set<NotificationsDbModel>().Remove(dbModel);
    }
}