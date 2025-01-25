using MatchThree.Domain.Interfaces.Notifications;
using MatchThree.Repository.MSSQL;
using MatchThree.Repository.MSSQL.Models;

namespace MatchThree.BL.Services.Notifications;

public class CreateNotificationsService(MatchThreeDbContext context) 
    : ICreateNotificationsService
{
    private readonly MatchThreeDbContext _context = context;

    public void Create(long userId)
    {
        _context.Set<NotificationsDbModel>().Add(new NotificationsDbModel
        {
            Id = userId,
            EnergyNotification = null
        });
    }
}