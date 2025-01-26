using System.Globalization;
using MatchThree.Domain.Interfaces;
using MatchThree.Domain.Interfaces.Notifications;
using MatchThree.Shared.Constants;
using MatchThree.Shared.Extensions;

namespace MatchThree.API.Services;

public sealed class NotificationService : IHostedService, IDisposable
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ITelegramBotService _telegramBotService;
    private readonly ILogger<NotificationService> _logger;
    private Timer? _timer;
    private bool _disposed;

    public NotificationService(IServiceProvider serviceProvider,
        ITelegramBotService telegramBotService,
        ILogger<NotificationService> logger)
    {
        _serviceProvider = serviceProvider;
        _telegramBotService = telegramBotService;
        _logger = logger;
    }
    
    public Task StartAsync(CancellationToken cancellationToken)
    {
        _timer = new Timer(async state => await SendNotificationsAsync(state),
            null,
            TimeSpan.Zero,
            EnergyConstants.Level1EnergyRecovery);
        
        return Task.CompletedTask;
    }
    
    private async Task SendNotificationsAsync(object? state)
    {
        using var scope = _serviceProvider.CreateScope();
        var readNotificationsService = scope.ServiceProvider.GetRequiredService<IReadNotificationsService>();
        var updateNotificationsService = scope.ServiceProvider.GetRequiredService<IUpdateNotificationsService>();
        var transactionsService = scope.ServiceProvider.GetRequiredService<ITransactionService>();

        try
        {
            var notificationsTargets = 
                await readNotificationsService.GetTargetsOfEnergyNotificationAsync();

            var groupedNotificationsTargets = notificationsTargets
                .GroupBy(x => x.Culture)
                .ToArray();

            foreach (var notificationsTargetsGroup in groupedNotificationsTargets)
            {
                var userAcceptLanguage = notificationsTargetsGroup.Key.ToAcceptLanguage();
                CultureInfo.CurrentCulture = new CultureInfo(userAcceptLanguage);
                CultureInfo.CurrentUICulture = new CultureInfo(userAcceptLanguage);
                
                foreach (var notificationTarget in notificationsTargetsGroup)
                {
                    try
                    {
                        await _telegramBotService.SendEnergyRecoveredNotification(notificationTarget.Id);
                    }
                    catch {}
                    finally
                    {
                        await updateNotificationsService.ResetEnergyNotificationTimeAsync(notificationTarget.Id);
                    }
                }
            }
            
            await transactionsService.CommitAsync();
            _logger.LogInformation($"{notificationsTargets.Count} notifications sent");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Something's wrong with the notifications:\n {ex}");
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _timer?.Change(Timeout.Infinite, 0);
        
        return Task.CompletedTask;
    }
    
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    
    private void Dispose(bool disposing)
    {
        if (_disposed) return;
        
        if (disposing)
            _timer?.Dispose();

        _disposed = true;
    }

    ~NotificationService()
    {
        Dispose(false);
    }
}