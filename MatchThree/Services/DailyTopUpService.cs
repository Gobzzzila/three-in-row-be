using MatchThree.Domain.Interfaces;

namespace MatchThree.API.Services;

public class DailyTopUpService : IHostedService, IDisposable
{
    private Timer? _timer;
    private bool _disposed;
    
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<CalculateLeaderboardService> _logger;
    
    public DailyTopUpService(IServiceProvider serviceProvider,
        ILogger<CalculateLeaderboardService> logger)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }
    
    public Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var timeProvider = scope.ServiceProvider.GetRequiredService<TimeProvider>();
        var now = timeProvider.GetUtcNow().DateTime;
        var nextMidnight = now.Date.AddDays(1);
        var initialDelay = nextMidnight - now;
        
        _timer = new Timer(async state => await TopUpEnergyDrinksAsync(state),
            null,
            initialDelay,
            TimeSpan.FromDays(1));

        return Task.CompletedTask;
    }
    
    private async Task TopUpEnergyDrinksAsync(object? state)
    {
        using var scope = _serviceProvider.CreateScope();
        var dailyRefillsService = scope.ServiceProvider.GetRequiredService<IDailyRefillsService>();
        
        try
        {
            await dailyRefillsService.ExecuteRefillEnergyDrinksAsync();
            
            _logger.LogInformation($"Energy drinks top upped");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Cannot top up energy drinks: {ex}");
        }
        
        try
        {
            await dailyRefillsService.ExecuteRefillAdsAsync();
            
            _logger.LogInformation($"Ads refilled");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Cannot refill ads: {ex}");
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

    ~DailyTopUpService()
    {
        Dispose(false);
    }
}