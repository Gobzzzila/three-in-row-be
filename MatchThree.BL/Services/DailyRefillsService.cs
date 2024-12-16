using MatchThree.Domain.Interfaces;
using MatchThree.Repository.MSSQL;
using MatchThree.Repository.MSSQL.Models;
using MatchThree.Shared.Constants;
using Microsoft.EntityFrameworkCore;

namespace MatchThree.BL.Services;

public class DailyRefillsService (MatchThreeDbContext context) 
    : IDailyRefillsService
{
    private readonly MatchThreeDbContext _context = context;

    public async Task ExecuteRefillEnergyDrinksAsync()
    {
        await using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            await context.Database.ExecuteSqlRawAsync(
                $"UPDATE [{EnergyConstants.EnergyTableName}] " +
                    $"SET [{nameof(EnergyDbModel.AvailableEnergyDrinkAmount)}] = CASE " +
                        $"WHEN [{nameof(EnergyDbModel.AvailableEnergyDrinkAmount)}] = '0' " +
                            $"THEN '{EnergyConstants.FreeEnergyDrinksPerDay}' " +
                        $"ELSE [{nameof(EnergyDbModel.AvailableEnergyDrinkAmount)}] " +
                    $"END, " +
                    $"[{nameof(EnergyDbModel.PurchasableEnergyDrinkAmount)}] = CASE " +
                        $"WHEN [{nameof(EnergyDbModel.PurchasableEnergyDrinkAmount)}] <> '{EnergyConstants.PurchasableEnergyDrinksPerDay}' " +
                            $"THEN '{EnergyConstants.PurchasableEnergyDrinksPerDay}' " +
                        $"ELSE [{nameof(EnergyDbModel.PurchasableEnergyDrinkAmount)}] " +
                    $"END " +
                $"WHERE [{nameof(EnergyDbModel.AvailableEnergyDrinkAmount)}] = '0' " +
                    $"OR [{nameof(EnergyDbModel.PurchasableEnergyDrinkAmount)}] <> '{EnergyConstants.PurchasableEnergyDrinksPerDay}';");
            
            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task ExecuteRefillAdsAsync()
    {
        await using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            await context.Database.ExecuteSqlRawAsync($"UPDATE [{AuthenticationConstants.UserTableName}] " +
                                                      $"SET [{nameof(UserDbModel.DailyAdAmount)}] = {AdConstants.DailyAdAmountValue} " +
                                                      $"WHERE [{nameof(UserDbModel.DailyAdAmount)}] <> {AdConstants.DailyAdAmountValue};");
            
            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}