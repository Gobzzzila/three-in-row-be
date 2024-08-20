using MatchThree.Domain.Interfaces.Energy;
using MatchThree.Domain.Models.Configuration;
using MatchThree.Repository.MSSQL;
using MatchThree.Repository.MSSQL.Models;
using MatchThree.Shared.Constants;

namespace MatchThree.BL.Services.Energy;

public class CreateEnergyService (MatchThreeDbContext context)
    : ICreateEnergyService
{
    public async Task CreateAsync(long userId)
    {
        var firstLevelParams = EnergyReserveConfiguration.GetStartValue();
        await context.Set<EnergyDbModel>().AddAsync(new EnergyDbModel
        {
            Id = userId,
            CurrentReserve = firstLevelParams.ReserveValue,
            MaxReserve = firstLevelParams.ReserveLevel,
            RecoveryLevel = EnergyRecoveryConfiguration.GetStartValue(),
            AvailableEnergyDrinkAmount = EnergyConstants.FreeEnergyDrinksPerDay,
            PurchasableEnergyDrinkAmount = EnergyConstants.PurchasableEnergyDrinksPerDay,
            LastRecoveryStartTime = null
        });
    }
}