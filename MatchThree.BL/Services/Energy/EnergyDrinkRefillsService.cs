using MatchThree.Domain.Interfaces.Energy;
using MatchThree.Repository.MSSQL;
using MatchThree.Repository.MSSQL.Models;
using MatchThree.Shared.Constants;
using Microsoft.EntityFrameworkCore;

namespace MatchThree.BL.Services.Energy;

public class EnergyDrinkRefillsService (MatchThreeDbContext context) 
    : IEnergyDrinkRefillsService
{
    private readonly MatchThreeDbContext _context = context;

    public Task RefillFreeEnergyDrinks()
    {
        return _context.Set<EnergyDbModel>()
            .Where(x => x.AvailableEnergyDrinkAmount == 0)
            .ExecuteUpdateAsync(x => 
                x.SetProperty(v => v.AvailableEnergyDrinkAmount, EnergyConstants.FreeEnergyDrinksPerDay));
    }

    public Task RefillPurchasableEnergyDrinks()
    {
        return _context.Set<EnergyDbModel>()
            .Where(x => x.PurchasableEnergyDrinkAmount != EnergyConstants.PurchasableEnergyDrinksPerDay)
            .ExecuteUpdateAsync(x => 
                x.SetProperty(v => v.PurchasableEnergyDrinkAmount, EnergyConstants.PurchasableEnergyDrinksPerDay));
    }
}