using MatchThree.Domain.Interfaces;

namespace MatchThree.API.Services;

public class TopUpFreeEnergyDrinksService : IHostedService, IDisposable
{
    private Timer? _timer;
    private bool _disposed;
    
    private readonly IServiceScope _scope;
    // private readonly IDeleteLeaderboardMemberService _deleteLeaderboardMemberService;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly ILogger<CalculateLeaderboardService> _logger;
    
    public TopUpFreeEnergyDrinksService(IServiceProvider serviceProvider,
        ILogger<CalculateLeaderboardService> logger)
    {
        _logger = logger;
        _scope = serviceProvider.CreateScope();
        var provider = _scope.ServiceProvider;
        _dateTimeProvider = provider.GetRequiredService<IDateTimeProvider>();
        // _deleteLeaderboardMemberService = provider.GetRequiredService<IDeleteLeaderboardMemberService>();
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
            
        }
        catch (Exception)
        {
            _logger.LogError("Cannot top up free energy drinks");
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

    ~TopUpFreeEnergyDrinksService()
    {
        Dispose(false);
    }
}