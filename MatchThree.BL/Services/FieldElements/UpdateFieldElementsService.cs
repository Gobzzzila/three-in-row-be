using MatchThree.BL.Configuration;
using MatchThree.Domain.Interfaces.Balance;
using MatchThree.Domain.Interfaces.FieldElements;
using MatchThree.Repository.MSSQL;
using MatchThree.Repository.MSSQL.Models;
using MatchThree.Shared.Exceptions;

namespace MatchThree.BL.Services.FieldElements;

public class UpdateFieldElementsService(MatchThreeDbContext context,
    IUpdateBalanceService updateBalanceService) 
    : IUpdateFieldElementsService
{
    private readonly MatchThreeDbContext _context = context;
    private readonly IUpdateBalanceService _updateBalanceService = updateBalanceService;

    public async Task UpgradeFieldAsync(long userId)
    {
        var dbModel = await _context.Set<FieldElementsDbModel>().FindAsync(userId);
        if (dbModel is null)
            throw new NoDataFoundException();
        
        var fieldParams = FieldConfiguration.GetParamsByLevel(dbModel!.FieldLevel);
        if (!fieldParams.NextLevel.HasValue)
            throw new MaxLevelReachedException();

        await _updateBalanceService.SpentBalanceAsync(userId, fieldParams.NextLevelCost!.Value);
        
        dbModel.FieldLevel = fieldParams.NextLevel.Value;
        _context.Set<FieldElementsDbModel>().Update(dbModel);
    }
}