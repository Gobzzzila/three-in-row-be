using AutoMapper;
using MatchThree.Domain.Configuration;
using MatchThree.Domain.Interfaces.Balance;
using MatchThree.Domain.Interfaces.Energy;
using MatchThree.Domain.Interfaces.Referral;
using MatchThree.Domain.Models;
using MatchThree.Repository.MSSQL;
using MatchThree.Repository.MSSQL.Models;
using MatchThree.Shared.Exceptions;

namespace MatchThree.BL.Services.Energy;

public class UpdateEnergyService(MatchThreeDbContext context,
    ISynchronizationEnergyService synchronizationEnergyService,
    IUpdateBalanceService updateBalanceService,
    IReadReferralService readReferralService,
    IMapper mapper) 
    : IUpdateEnergyService
{
    private readonly MatchThreeDbContext _context = context;
    private readonly ISynchronizationEnergyService _synchronizationEnergyService = synchronizationEnergyService;
    private readonly IUpdateBalanceService _updateBalanceService = updateBalanceService;
    private readonly IReadReferralService _readReferralService = readReferralService;
    private readonly IMapper _mapper = mapper;

    public async Task UpgradeReserveAsync(long userId)
    {
        var dbModel = await _context.Set<EnergyDbModel>().FindAsync(userId);
        if (dbModel is null)
            throw new NoDataFoundException();
        
        var reserveParams = EnergyReserveConfiguration.GetParamsByLevel(dbModel!.MaxReserve);
        if (!reserveParams.NextLevel.HasValue)
            throw new MaxLevelReachedException();
        
        if (reserveParams.UpgradeCondition is not null) 
            if (!await reserveParams.UpgradeCondition(_readReferralService, userId)) 
                throw new UpgradeConditionsException();
        
        await _updateBalanceService.SpentBalanceAsync(userId, reserveParams.NextLevelCost!.Value);
        
        _synchronizationEnergyService.SynchronizeModel(dbModel);
        dbModel.MaxReserve = reserveParams.NextLevel.Value;
        var newMaxReserve = EnergyReserveConfiguration.GetReserveMaxValue(reserveParams.NextLevel.Value);
        dbModel.CurrentReserve += (newMaxReserve - reserveParams.MaxReserve);
        
        _context.Set<EnergyDbModel>().Update(dbModel);
    }

    public async Task UpgradeRecoveryAsync(long id)
    {
        var dbModel = await _context.Set<EnergyDbModel>().FindAsync(id);
        if (dbModel is null)
            throw new NoDataFoundException();
        
        var recoveryParams = EnergyRecoveryConfiguration.GetParamsByLevel(dbModel!.RecoveryLevel);
        if (!recoveryParams.NextLevel.HasValue)
            throw new MaxLevelReachedException();

        if (!recoveryParams.UpgradeCondition!(dbModel)) 
            throw new UpgradeConditionsException();
        
        await _updateBalanceService.SpentBalanceAsync(id, recoveryParams.NextLevelCost!.Value);
        
        _synchronizationEnergyService.SynchronizeModel(dbModel);
        dbModel.RecoveryLevel = recoveryParams.NextLevel.Value;
        _synchronizationEnergyService.SynchronizeModel(dbModel);

        _context.Set<EnergyDbModel>().Update(dbModel);
    }

    public async Task<EnergyEntity> UseEnergyDrinkAsync(long id)
    {
        var dbModel = await _context.Set<EnergyDbModel>().FindAsync(id);
        if (dbModel is null)
            throw new NoDataFoundException();
        
        if (dbModel.AvailableEnergyDrinkAmount < 1)
            throw new NotEnoughBalanceException();
        
        var maxReserve = EnergyReserveConfiguration.GetReserveMaxValue(dbModel!.MaxReserve);
        _synchronizationEnergyService.SynchronizeModel(dbModel);
        dbModel.CurrentReserve += maxReserve;
        dbModel.LastRecoveryStartTime = null;
        dbModel.AvailableEnergyDrinkAmount -= 1;
        
        var result = _context.Set<EnergyDbModel>().Update(dbModel);
        return _mapper.Map<EnergyEntity>(result.Entity);
    }
    
    public async Task PurchaseEnergyDrinkAsync(long id)
    {
        var dbModel = await _context.Set<EnergyDbModel>().FindAsync(id);
        if (dbModel is null)
            throw new NoDataFoundException();
        
        if (dbModel.PurchasableEnergyDrinkAmount < 1)
            throw new NotEnoughBalanceException();
        
        _synchronizationEnergyService.SynchronizeModel(dbModel);
        dbModel.PurchasableEnergyDrinkAmount -=1;
        dbModel.AvailableEnergyDrinkAmount += 1;
        
        _context.Set<EnergyDbModel>().Update(dbModel);
    }
}