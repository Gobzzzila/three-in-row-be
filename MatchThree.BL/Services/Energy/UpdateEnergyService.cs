using MatchThree.BL.Configuration;
using MatchThree.Domain.Interfaces.Balance;
using MatchThree.Domain.Interfaces.Energy;
using MatchThree.Domain.Interfaces.Notifications;
using MatchThree.Domain.Interfaces.Upgrades;
using MatchThree.Repository.MSSQL;
using MatchThree.Repository.MSSQL.Models;
using MatchThree.Shared.Constants;
using MatchThree.Shared.Exceptions;

namespace MatchThree.BL.Services.Energy;

public class UpdateEnergyService(MatchThreeDbContext context,
    ISynchronizationEnergyService synchronizationEnergyService,
    IUpdateBalanceService updateBalanceService,
    IUpgradesRestrictionsService upgradesRestrictionsService,
    IUpdateNotificationsService updateNotificationsService,
    TimeProvider timeProvider) 
    : IUpdateEnergyService
{
    private readonly MatchThreeDbContext _context = context;
    private readonly ISynchronizationEnergyService _synchronizationEnergyService = synchronizationEnergyService;
    private readonly IUpdateBalanceService _updateBalanceService = updateBalanceService;
    private readonly IUpgradesRestrictionsService _upgradesRestrictionsService = upgradesRestrictionsService;
    private readonly IUpdateNotificationsService _updateNotificationsService = updateNotificationsService;
    private readonly TimeProvider _timeProvider = timeProvider;

    public async Task UpgradeReserveAsync(long userId)
    {
        var dbModel = await _context.Set<EnergyDbModel>().FindAsync(userId);
        if (dbModel is null)
            throw new NoDataFoundException();
        
        var reserveParams = EnergyReserveConfiguration.GetParamsByLevel(dbModel!.MaxReserve);
        if (!reserveParams.NextLevel.HasValue)
            throw new MaxLevelReachedException();

        if (reserveParams.UpgradeCondition is not null)
        {
            var missingAmountOfReferrals = await reserveParams.UpgradeCondition(_upgradesRestrictionsService, userId);
            if (missingAmountOfReferrals is not null) 
                throw new UpgradeConditionsException();
        }
        
        await _updateBalanceService.SpendBalanceAsync(userId, reserveParams.NextLevelCost!.Value);
        
        _synchronizationEnergyService.SynchronizeModel(dbModel);
        dbModel.MaxReserve = reserveParams.NextLevel.Value;
        var newMaxReserve = EnergyReserveConfiguration.GetReserveMaxValue(reserveParams.NextLevel.Value);
        dbModel.CurrentReserve += (newMaxReserve - reserveParams.MaxReserve);
        
        await UpdateEnergyNotificationTime(dbModel);
        
        _context.Set<EnergyDbModel>().Update(dbModel);
    }

    public async Task UpgradeRecoveryAsync(long userId)
    {
        var dbModel = await _context.Set<EnergyDbModel>().FindAsync(userId);
        if (dbModel is null)
            throw new NoDataFoundException();
        
        var recoveryParams = EnergyRecoveryConfiguration.GetParamsByLevel(dbModel.RecoveryLevel);
        if (!recoveryParams.NextLevel.HasValue)
            throw new MaxLevelReachedException();

        var requiredReserveLevel = recoveryParams.UpgradeCondition?.Invoke(_upgradesRestrictionsService, dbModel.MaxReserve);
        if (requiredReserveLevel != null) 
            throw new UpgradeConditionsException();

        await _updateBalanceService.SpendBalanceAsync(userId, recoveryParams.NextLevelCost!.Value);
        
        _synchronizationEnergyService.SynchronizeModel(dbModel);
        dbModel.RecoveryLevel = recoveryParams.NextLevel.Value;
        _synchronizationEnergyService.SynchronizeModel(dbModel);

        await UpdateEnergyNotificationTime(dbModel);
        
        _context.Set<EnergyDbModel>().Update(dbModel);
    }

    public async Task UseEnergyDrinkAsync(long userId)
    {
        var dbModel = await _context.Set<EnergyDbModel>().FindAsync(userId);
        if (dbModel is null)
            throw new NoDataFoundException();
        
        if (dbModel.AvailableEnergyDrinkAmount < 1)
            throw new NotEnoughBalanceException();
        
        var reserveMaxValue = EnergyReserveConfiguration.GetReserveMaxValue(dbModel.MaxReserve);
        _synchronizationEnergyService.SynchronizeModel(dbModel);
        dbModel.CurrentReserve += reserveMaxValue;
        dbModel.LastRecoveryStartTime = null;
        dbModel.AvailableEnergyDrinkAmount -= 1;
        dbModel.UsedEnergyDrinkCounter += 1;
        
        await UpdateEnergyNotificationTime(dbModel);

        _context.Set<EnergyDbModel>().Update(dbModel);
    }
    
    public async Task PurchaseEnergyDrinkAsync(long userId)
    {
        var dbModel = await _context.Set<EnergyDbModel>().FindAsync(userId);
        if (dbModel is null)
            throw new NoDataFoundException();
        
        if (dbModel.PurchasableEnergyDrinkAmount < 1)
            throw new NotEnoughBalanceException();
        
        _synchronizationEnergyService.SynchronizeModel(dbModel);
        dbModel.PurchasableEnergyDrinkAmount -=1;
        dbModel.AvailableEnergyDrinkAmount += 1;
        dbModel.PurchasedEnergyDrinkCounter += 1;
        
        await UseEnergyDrinkAsync(userId);
    }
    
    public async Task SpendEnergyAsync(long userId)
    {
        var dbModel = await _context.Set<EnergyDbModel>().FindAsync(userId);
        if (dbModel is null)
            throw new NoDataFoundException();
        
        _synchronizationEnergyService.SynchronizeModel(dbModel);
        if (dbModel.CurrentReserve == 0)
            throw new ValidationException(TranslationConstants.ExceptionNotEnoughEnergyTextKey);
        
        dbModel.CurrentReserve -=1;
        
        var reserveMaxValue = EnergyReserveConfiguration.GetReserveMaxValue(dbModel.MaxReserve);
        if (reserveMaxValue > dbModel.CurrentReserve && dbModel.LastRecoveryStartTime is null)
            dbModel.LastRecoveryStartTime = _timeProvider.GetUtcNow().DateTime;
        
        await UpdateEnergyNotificationTime(dbModel);
        
        _context.Set<EnergyDbModel>().Update(dbModel);
    }
    
    public async Task TopUpEnergyForAdAsync(long userId)
    {
        var dbModel = await _context.Set<EnergyDbModel>().FindAsync(userId);
        if (dbModel is null)
            throw new NoDataFoundException();
        
        _synchronizationEnergyService.SynchronizeModel(dbModel);
        dbModel.CurrentReserve += AdConstants.EnergyPerAd;
        
        var reserveMaxValue = EnergyReserveConfiguration.GetReserveMaxValue(dbModel.MaxReserve);
        if (dbModel.CurrentReserve >= reserveMaxValue)
            dbModel.LastRecoveryStartTime = null;

        await UpdateEnergyNotificationTime(dbModel);
        
        _context.Set<EnergyDbModel>().Update(dbModel);
    }

    private async Task UpdateEnergyNotificationTime(EnergyDbModel dbModel)
    {
        if (dbModel.LastRecoveryStartTime is null)
        {
            await _updateNotificationsService.SetEnergyNotificationTimeAsync(dbModel.Id, null);
            return;            
        }
        
        var reserveMaxValue = EnergyReserveConfiguration.GetReserveMaxValue(dbModel.MaxReserve);
        var recoveryTime = EnergyRecoveryConfiguration.GetRecoveryTime(dbModel.RecoveryLevel);

        var notificationTime = dbModel.LastRecoveryStartTime + (reserveMaxValue - dbModel.CurrentReserve) * recoveryTime;
        await _updateNotificationsService.SetEnergyNotificationTimeAsync(dbModel.Id, notificationTime!.Value);
    }
}