using MatchThree.Domain.Interfaces.User;
using MatchThree.Domain.Models;
using MatchThree.Repository.MSSQL;
using MatchThree.Repository.MSSQL.Models;
using MatchThree.Shared.Exceptions;

namespace MatchThree.BL.Services.User;

public class UpdateUserService(MatchThreeDbContext context) : IUpdateUserService
{
    private readonly MatchThreeDbContext _context = context;

    public async Task SyncUserData(UserEntity entity)
    {
        var dbModel = await _context.Set<UserDbModel>()
            .FindAsync(entity.Id);
        
        if (dbModel is null)
            throw new NoDataFoundException();

        dbModel.IsPremium = entity.IsPremium;
        dbModel.FirstName = entity.FirstName;
        dbModel.Username = entity.Username;
        
        _context.Set<UserDbModel>().Update(dbModel);
    }
}