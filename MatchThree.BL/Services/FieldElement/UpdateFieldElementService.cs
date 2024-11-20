using MatchThree.BL.Configuration;
using MatchThree.Domain.Interfaces.Balance;
using MatchThree.Domain.Interfaces.FieldElement;
using MatchThree.Repository.MSSQL;
using MatchThree.Repository.MSSQL.Models;
using MatchThree.Shared.Enums;
using MatchThree.Shared.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace MatchThree.BL.Services.FieldElement;

public class UpdateFieldElementService(MatchThreeDbContext context, 
    IUpdateBalanceService updateBalanceService) 
    : IUpdateFieldElementService
{
    private readonly MatchThreeDbContext _context = context;
    private readonly IUpdateBalanceService _updateBalanceService = updateBalanceService;

    public async Task UpgradeFieldElementAsync(long userId, CryptoTypes cryptoType)
    {
        var dbModel = await _context.Set<FieldElementDbModel>()
            .SingleOrDefaultAsync(x => x.UserId == userId && x.Element == cryptoType);
        
        if (dbModel is null)
            throw new NoDataFoundException();
        
        if (dbModel.Level == ElementLevels.Undefined) 
            throw new UpgradeConditionsException();
        
        var fieldElementParams = FieldElementsConfiguration.GetParamsByTypeAndLevel(dbModel.Element, dbModel.Level);
        if (!fieldElementParams.NextLevel.HasValue)
            throw new MaxLevelReachedException();
        
        await _updateBalanceService.SpendBalanceAsync(userId, fieldElementParams.NextLevelCost!.Value);
        dbModel.Level = fieldElementParams.NextLevel!.Value;

        _context.Set<FieldElementDbModel>().Update(dbModel);
    }
    
    public async Task UnlockFieldElementAsync(long userId, CryptoTypes cryptoType)
    {
        var dbModel = await _context.Set<FieldElementDbModel>()
            .SingleOrDefaultAsync(x => x.UserId == userId && x.Element == cryptoType);
        
        if (dbModel is null)
            throw new NoDataFoundException();
        
        if (dbModel.Level != ElementLevels.Undefined) 
            throw new ValidationException();
        
        dbModel.Level = ElementLevels.Level1;
        _context.Set<FieldElementDbModel>().Update(dbModel);
    }
}