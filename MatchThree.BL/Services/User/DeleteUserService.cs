using MatchThree.Domain.Interfaces.Balance;
using MatchThree.Domain.Interfaces.FieldElements;
using MatchThree.Domain.Interfaces.Referral;
using MatchThree.Domain.Interfaces.User;
using MatchThree.Repository.MSSQL;
using MatchThree.Repository.MSSQL.Models;
using MatchThree.Shared.Exceptions;

namespace MatchThree.BL.Services.User;

public class DeleteUserService (MatchThreeDbContext context,
    IDeleteReferralService deleteReferralService,
    IDeleteBalanceService deleteBalanceService,
    IDeleteFieldElementsService deleteFieldElementsService)
    : IDeleteUserService
{
    private readonly MatchThreeDbContext _context = context;
    private readonly IDeleteReferralService _deleteReferralService = deleteReferralService;
    private readonly IDeleteBalanceService _deleteBalanceService = deleteBalanceService;
    private readonly IDeleteFieldElementsService _deleteFieldElementsService = deleteFieldElementsService;

    public async Task DeleteAsync(long id)
    {
        var dbModel = await _context.Set<UserDbModel>().FindAsync(id);
        if (dbModel is null)
            throw new NoDataFoundException();

        await _deleteReferralService.DeleteByUserIdAsync(id);
        await _deleteBalanceService.DeleteAsync(id);
        await _deleteFieldElementsService.DeleteAsync(id);
        _context.Set<UserDbModel>().Remove(dbModel);
    }
}