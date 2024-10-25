using MatchThree.BL.Configuration;
using MatchThree.Domain.Interfaces.Balance;
using MatchThree.Domain.Interfaces.Referral;
using MatchThree.Repository.MSSQL;
using MatchThree.Repository.MSSQL.Models;
using MatchThree.Shared.Exceptions;

namespace MatchThree.BL.Services.Balance;

public class UpdateBalanceService (MatchThreeDbContext context,
    IReadReferralService readReferralService) 
    : IUpdateBalanceService
{
    private readonly MatchThreeDbContext _context = context;
    private readonly IReadReferralService _readReferralService = readReferralService;

    public async Task SpendBalanceAsync(long id, uint amount)
    {
        var dbModel = await _context.Set<BalanceDbModel>().FindAsync(id);
        if (dbModel is null)
            throw new NoDataFoundException();
        
        if (dbModel.Balance < amount)
            throw new NotEnoughBalanceException();
        
        dbModel.Balance -= amount;
        _context.Set<BalanceDbModel>().Update(dbModel);
    }
    
    public async Task AddBalanceAsync(long id, uint amount)
    {
        var dbModel = await _context.Set<BalanceDbModel>().FindAsync(id);
        if (dbModel is null)
            throw new NoDataFoundException();
        
        await UpdateReferrerBalance(id, dbModel.OverallBalance, amount);
        dbModel.Balance += amount;
        dbModel.OverallBalance += amount;
        
        _context.Set<BalanceDbModel>().Update(dbModel);
    }

    private async ValueTask UpdateReferrerBalance(long id, ulong overallBalance, uint amountToAdd)
    {
        var result = LeagueConfiguration.IsLeagueUpped(overallBalance, amountToAdd);
        if (!result.isUpped)
            return;
        
        var referrerId = await _readReferralService.GetReferrerIdByReferralIdAsync(id);
        if (!referrerId.HasValue)
            return;
        
        await AddBalanceAsync(referrerId.Value, result.rewardForReferrer);
    }
}