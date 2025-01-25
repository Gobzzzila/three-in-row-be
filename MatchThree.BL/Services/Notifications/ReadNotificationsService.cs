using AutoMapper;
using MatchThree.Domain.Interfaces.Notifications;
using MatchThree.Domain.Models;
using MatchThree.Repository.MSSQL;
using MatchThree.Repository.MSSQL.Models;
using Microsoft.EntityFrameworkCore;

namespace MatchThree.BL.Services.Notifications;

public class ReadNotificationsService(MatchThreeDbContext context, 
    IMapper mapper,
    TimeProvider timeProvider) 
    : IReadNotificationsService
{
    private readonly MatchThreeDbContext _context = context;
    private readonly IMapper _mapper = mapper;
    private readonly TimeProvider _timeProvider = timeProvider;

    public async Task<IReadOnlyCollection<NotificationsEntity>> GetTargetsOfEnergyNotificationAsync()
    {
        var now = _timeProvider.GetUtcNow().DateTime;
        var dbModels = await _context.Set<NotificationsDbModel>()
            .Include(x => x.User)
            .ThenInclude(x => x!.Settings)
            .Where(x => x.User!.Settings!.Notifications 
                        && x.EnergyNotification <= now)
            .ToListAsync();
        
        return _mapper.Map<IReadOnlyCollection<NotificationsEntity>>(dbModels);
    }
}