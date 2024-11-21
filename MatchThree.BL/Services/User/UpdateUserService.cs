using MatchThree.Domain.Interfaces.User;
using MatchThree.Domain.Models;
using MatchThree.Repository.MSSQL;
using MatchThree.Repository.MSSQL.Models;
using MatchThree.Shared.Constants;
using MatchThree.Shared.Exceptions;

namespace MatchThree.BL.Services.User;

public class UpdateUserService(MatchThreeDbContext context, 
    TimeProvider timeProvider) 
    : IUpdateUserService
{
    private readonly MatchThreeDbContext _context = context;
    private readonly TimeProvider _timeProvider = timeProvider;

    public async Task SyncUserData(UserEntity entity)
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
}