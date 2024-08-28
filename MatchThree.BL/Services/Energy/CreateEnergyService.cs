using MatchThree.Domain.Configuration;
using MatchThree.Domain.Interfaces.Energy;
using MatchThree.Repository.MSSQL;
using MatchThree.Repository.MSSQL.Models;
using MatchThree.Shared.Constants;

namespace MatchThree.BL.Services.Energy;

public class CreateEnergyService (MatchThreeDbContext context)
    : ICreateEnergyService
{
    private readonly MatchThreeDbContext _context = context;

    public void Create(long userId)
    {
        var firstLevelParams = EnergyReserveConfiguration.GetStartValue();
        _context.Set<EnergyDbModel>().Add(new EnergyDbModel
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