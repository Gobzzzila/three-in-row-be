using MatchThree.BL.Configuration;
using MatchThree.Domain.Interfaces.Balance;
using MatchThree.Domain.Interfaces.Field;
using MatchThree.Repository.MSSQL;
using MatchThree.Repository.MSSQL.Models;
using MatchThree.Shared.Exceptions;

namespace MatchThree.BL.Services.Field;

public class UpdateFieldService(MatchThreeDbContext context,
    IUpdateBalanceService updateBalanceService) 
    : IUpdateFieldService
{
    private readonly MatchThreeDbContext _context = context;
    private readonly IUpdateBalanceService _updateBalanceService = updateBalanceService;

    public async Task UpgradeFieldAsync(long userId)
    {
        var dbModel = await _context.Set<FieldDbModel>().FindAsync(userId);
        if (dbModel is null)
            throw new NoDataFoundException();
        
        var fieldParams = FieldConfiguration.GetParamsByLevel(dbModel!.FieldLevel);
        if (!fieldParams.NextLevel.HasValue)
            throw new MaxLevelReachedException();

        await _updateBalanceService.SpendBalanceAsync(userId, fieldParams.NextLevelCost!.Value);
        
        var random = new Random();
        dbModel.Field[fieldParams.NextLevelCoordinates.Y][fieldParams.NextLevelCoordinates.X] = random.Next(1, 6);
        dbModel.FieldLevel = fieldParams.NextLevel.Value;

        _context.Set<FieldDbModel>().Update(dbModel);
    }
}