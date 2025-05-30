﻿using MatchThree.BL.Configuration;
using MatchThree.Domain.Interfaces.Balance;
using MatchThree.Domain.Interfaces.Field;
using MatchThree.Domain.Interfaces.FieldElement;
using MatchThree.Repository.MSSQL;
using MatchThree.Repository.MSSQL.Models;
using MatchThree.Shared.Enums;
using MatchThree.Shared.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace MatchThree.BL.Services.Field;

public class UpdateFieldService(MatchThreeDbContext context,
    IUpdateBalanceService updateBalanceService,
    IUpdateFieldElementService updateFieldElementService)
    : IUpdateFieldService
{
    private readonly MatchThreeDbContext _context = context;
    private readonly IUpdateBalanceService _updateBalanceService = updateBalanceService;
    private readonly IUpdateFieldElementService _updateFieldElementService = updateFieldElementService;

    public async Task UpgradeFieldAsync(long userId)
    {
        var dbModel = await _context.Set<FieldDbModel>()
            .Include(x => x.FieldElements!.Where(y => y.Level != ElementLevels.Undefined))
            .FirstOrDefaultAsync(x => x.Id == userId);
        if (dbModel?.FieldElements is null)
            throw new NoDataFoundException();

        var fieldParams = FieldConfiguration.GetParamsByLevel(dbModel!.FieldLevel);
        if (!fieldParams.NextLevel.HasValue)
            throw new MaxLevelReachedException();

        if (fieldParams.NextLevelNewCrypto.HasValue)
            await _updateFieldElementService.UnlockFieldElementAsync(userId, fieldParams.NextLevelNewCrypto.Value);

        await _updateBalanceService.SpendBalanceAsync(userId, fieldParams.NextLevelCost!.Value);

        var existedCryptoTypes = dbModel.FieldElements.Select(x => (int)x.Element).ToList(); 
        dbModel.Field[fieldParams.NextLevelCoordinates.Y][fieldParams.NextLevelCoordinates.X] = 
            GetUniqueValue(dbModel.Field, fieldParams.NextLevelCoordinates.Y, fieldParams.NextLevelCoordinates.X, existedCryptoTypes);
        dbModel.FieldLevel = fieldParams.NextLevel.Value;

        _context.Set<FieldDbModel>().Update(dbModel);
    }
    
    private static int GetUniqueValue(int[][] array, int y, int x, List<int> cryptoTypes)
    {
        var possibleValues = new HashSet<int>(cryptoTypes);
        if (y > 0)
            possibleValues.Remove(array[y - 1][x]); 
        
        if (y < array.Length - 1) 
            possibleValues.Remove(array[y + 1][x]); 
        
        if (x > 0) 
            possibleValues.Remove(array[y][x - 1]); 
        
        if (x < array[y].Length - 1) 
            possibleValues.Remove(array[y][x + 1]); 
        
        var random = new Random();
        return possibleValues.ElementAt(random.Next(possibleValues.Count));
    }

    public async Task UpdateFieldAsync(long userId, int[][] field)
    {
        if (field.Length != 9 || field.Any(x => x.Length != 9))
            throw new ValidationException();

        var dbModel = await _context.Set<FieldDbModel>()
            .Include(x => x.FieldElements!.Where(y => y.Level != ElementLevels.Undefined))
            .FirstOrDefaultAsync(x => x.Id == userId);
        if (dbModel?.FieldElements is null)
            throw new NoDataFoundException();

        var currentCryptoTypesAsInt = dbModel.FieldElements.Select(x => (int)x.Element).ToList(); 
        if (!field.All(x => x.All(y => y == 0 || currentCryptoTypesAsInt.Contains(y))))
            throw new ValidationException();
        
        var fieldParams = FieldConfiguration.GetParamsByLevel(dbModel.FieldLevel);
        if (field.Sum(x => x.Count(y => y != 0)) != fieldParams.AmountOfCells)
            throw new ValidationException();
        
        dbModel.Field = field;
        dbModel.MoveСounter += 1;
        _context.Set<FieldDbModel>().Update(dbModel);
    }
}