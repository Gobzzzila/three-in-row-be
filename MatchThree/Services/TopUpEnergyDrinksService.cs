using MatchThree.Domain.Interfaces;
using MatchThree.Domain.Interfaces.Energy;

namespace MatchThree.API.Services;

public class TopUpEnergyDrinksService : IHostedService, IDisposable
{
    private Timer? _timer;
    private bool _disposed;
    
    private readonly IServiceScope _scope;
    private readonly IEnergyDrinkRefillsService _energyDrinkRefillsService;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly ILogger<CalculateLeaderboardService> _logger;
    
    public TopUpEnergyDrinksService(IServiceProvider serviceProvider,
        ILogger<CalculateLeaderboardService> logger)
    {
        _logger = logger;
        _scope = serviceProvider.CreateScope();
        var provider = _scope.ServiceProvider;
        _dateTimeProvider = provider.GetRequiredService<IDateTimeProvider>();
        _energyDrinkRefillsService = provider.GetRequiredService<IEnergyDrinkRefillsService>();
    }
    
    public Task StartAsync(CancellationToken cancellationToken)
    {
        var now = _dateTimeProvider.GetUtcDateTime();
        var nextMidnight = now.Date.AddDays(1);
        var initialDelay = nextMidnight - now;
        
        _timer = new Timer(async state => await TopUpEnergyDrinks(state),
            null,
            initialDelay,
            TimeSpan.FromDays(1));

        return Task.CompletedTask;
    }
    
    private async Task TopUpEnergyDrinks(object? state)
    {
        try
        {
            await _energyDrinkRefillsService.RefillFreeEnergyDrinks();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Cannot top up free energy drinks, {ex}");
        }
        
        try
        {
            await _energyDrinkRefillsService.RefillPurchasableEnergyDrinks();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Cannot top up purchasable energy drinks, {ex}");
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
        {
            _scope.Dispose();
            _timer?.Dispose();
        }

        _disposed = true;
    }

    ~TopUpEnergyDrinksService()
    {
        Dispose(false);
    }
}