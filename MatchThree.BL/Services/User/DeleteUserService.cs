using MatchThree.Domain.Interfaces.Balance;
using MatchThree.Domain.Interfaces.Referral;
using MatchThree.Domain.Interfaces.User;
using MatchThree.Repository.MSSQL;
using MatchThree.Repository.MSSQL.Models;
using MatchThree.Shared.Exceptions;

namespace MatchThree.BL.Services.User;

public class DeleteUserService (MatchThreeDbContext context,
    IDeleteReferralService deleteReferralService,
    IDeleteBalanceService deleteBalanceService)
    : IDeleteUserService
{
    public async Task DeleteAsync(long id)
    {
        var dbModel = await context.Set<UserDbModel>().FindAsync(id);
        if (dbModel is null)
            throw new NoDataFoundException();

        await deleteReferralService.DeleteByUserIdAsync(id);
        await deleteBalanceService.DeleteAsync(id);
        context.Set<UserDbModel>().Remove(dbModel);
    }
}