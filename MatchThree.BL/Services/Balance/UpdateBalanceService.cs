using MatchThree.Domain.Interfaces.Balance;
using MatchThree.Domain.Interfaces.Referral;
using MatchThree.Domain.Models.Configuration;
using MatchThree.Repository.MSSQL;
using MatchThree.Repository.MSSQL.Models;
using MatchThree.Shared.Exceptions;

namespace MatchThree.BL.Services.Balance;

public class UpdateBalanceService (MatchThreeDbContext context,
    IReadReferralService readReferralService) 
    : IUpdateBalanceService
{
    public async Task SpentBalanceAsync(long id, uint amount)
    {
        var dbModel = (await context.Set<BalanceDbModel>().FindAsync(id))!;

        if (dbModel.Balance < amount)
            throw new NotEnoughBalanceException();
        
        dbModel.Balance -= amount;
        context.Set<BalanceDbModel>().Update(dbModel);
    }
    
    public async Task AddBalanceAsync(long id, uint amount)
    {
        var dbModel = (await context.Set<BalanceDbModel>().FindAsync(id))!;
        await UpdateReferrerBalance(id, dbModel.OverallBalance, amount);
        dbModel.Balance += amount;
        dbModel.OverallBalance += amount;
        
        context.Set<BalanceDbModel>().Update(dbModel);
    }

    private async ValueTask UpdateReferrerBalance(long id, ulong overallBalance, uint amountToAdd)
    {
        var result = LeagueConfiguration.IsLeagueUpped(overallBalance, amountToAdd);
        if (!result.isUpped)
            return;
        
        var referrerId = await readReferralService.ReferrerIdByReferralIdAsync(id);
        if (!referrerId.HasValue)
            return;
        
        await AddBalanceAsync(referrerId.Value, result.rewardForReferrer);
    }
}