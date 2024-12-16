using MatchThree.Domain.Interfaces.Energy;
using MatchThree.Domain.Interfaces.User;
using MatchThree.Domain.Models;
using MatchThree.Repository.MSSQL;
using MatchThree.Repository.MSSQL.Models;
using MatchThree.Shared.Constants;
using MatchThree.Shared.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace MatchThree.BL.Services.User;

public class UpdateUserService(MatchThreeDbContext context, 
    IUpdateEnergyService updateEnergyService,
    TimeProvider timeProvider) 
    : IUpdateUserService
{
    private readonly MatchThreeDbContext _context = context;
    private readonly IUpdateEnergyService _updateEnergyService = updateEnergyService;
    private readonly TimeProvider _timeProvider = timeProvider;

    public async Task SyncUserDataAsync(UserEntity entity)
    {
        var dbModel = await _context.Set<UserDbModel>().FindAsync(entity.Id);
        if (dbModel is null)
            throw new NoDataFoundException();

        if (dbModel.BannedUntil is not null)
        {
            var dateTimeNow = _timeProvider.GetUtcNow().DateTime;
            if (dbModel.BannedUntil > dateTimeNow)
                throw new ValidationException(TranslationConstants.ExceptionBannedTextKey, [Math.Round((dbModel.BannedUntil.Value - dateTimeNow).TotalHours)]);
        }

        dbModel.IsPremium = entity.IsPremium;
        dbModel.FirstName = entity.FirstName;
        dbModel.Username = entity.Username;
        dbModel.SessionHash = entity.SessionHash;
        
        _context.Set<UserDbModel>().Update(dbModel);
    }

    public async Task RecordAdViewAsync(long userId)
    {
        var dbModel = await _context.Set<UserDbModel>()
            .Include(x => x.Energy)
            .FirstOrDefaultAsync(x => x.Id == userId);
        if (dbModel is null)
            throw new NoDataFoundException();

        if (dbModel.DailyAdAmount <= 0)
            throw new ValidationException(TranslationConstants.UpgradeEnergyDrinkBlockingTextKey);

        dbModel.DailyAdAmount -= 1;
        _context.Set<UserDbModel>().Update(dbModel);

        await _updateEnergyService.TopUpEnergyForAdAsync(userId);
    }
}