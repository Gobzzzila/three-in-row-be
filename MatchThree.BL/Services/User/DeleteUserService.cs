using MatchThree.Domain.Interfaces.Balance;
using MatchThree.Domain.Interfaces.DailyLogin;
using MatchThree.Domain.Interfaces.Energy;
using MatchThree.Domain.Interfaces.Field;
using MatchThree.Domain.Interfaces.FieldElement;
using MatchThree.Domain.Interfaces.Quests;
using MatchThree.Domain.Interfaces.Referral;
using MatchThree.Domain.Interfaces.User;
using MatchThree.Repository.MSSQL;
using MatchThree.Repository.MSSQL.Models;
using MatchThree.Shared.Exceptions;

namespace MatchThree.BL.Services.User;

public class DeleteUserService(
    MatchThreeDbContext context,
    IDeleteReferralService deleteReferralService,
    IDeleteBalanceService deleteBalanceService,
    IDeleteFieldService deleteFieldService,
    IDeleteEnergyService deleteEnergyService,
    IDeleteCompletedQuestsService deleteCompletedQuestsService,
    IDeleteFieldElementService deleteFieldElementService,
    IDeleteDailyLoginService deleteDailyLoginService)
    : IDeleteUserService
{
    private readonly MatchThreeDbContext _context = context;
    private readonly IDeleteReferralService _deleteReferralService = deleteReferralService;
    private readonly IDeleteBalanceService _deleteBalanceService = deleteBalanceService;
    private readonly IDeleteFieldService _deleteFieldService = deleteFieldService;
    private readonly IDeleteEnergyService _deleteEnergyService = deleteEnergyService;
    private readonly IDeleteCompletedQuestsService _deleteCompletedQuestsService = deleteCompletedQuestsService;
    private readonly IDeleteFieldElementService _deleteFieldElementService = deleteFieldElementService;
    private readonly IDeleteDailyLoginService _deleteDailyLoginService = deleteDailyLoginService;

    public async Task DeleteAsync(long id)
    {
        var dbModel = await _context.Set<UserDbModel>().FindAsync(id);
        if (dbModel is null)
            throw new NoDataFoundException();

        await _deleteEnergyService.DeleteAsync(id);
        await _deleteReferralService.DeleteByUserIdAsync(id);
        await _deleteBalanceService.DeleteAsync(id);
        await _deleteFieldService.DeleteAsync(id);
        await _deleteFieldElementService.DeleteByUserIdAsync(id);
        await _deleteCompletedQuestsService.DeleteByUserIdAsync(id);
        await _deleteDailyLoginService.DeleteAsync(id);
        _context.Set<UserDbModel>().Remove(dbModel);
    }
}