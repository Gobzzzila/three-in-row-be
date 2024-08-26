using MatchThree.Domain.Interfaces;
using MatchThree.Domain.Interfaces.LeaderboardMember;
using MatchThree.Domain.Models.Configuration;
using MatchThree.Shared.Enums;

namespace MatchThree.API.Services;

public class CalculateLeaderboardService : IHostedService, IDisposable
{
    private Timer? _timer;
    private bool _disposed;
    private LeagueTypes _lastProcessedLeague = 0;

    private readonly IServiceScope _scope;
    private readonly ITransactionService _transactionService;
    private readonly ICreateLeaderboardMemberService _createLeaderboardMemberService;
    private readonly IDeleteLeaderboardMemberService _deleteLeaderboardMemberService;
    private readonly ILogger<CalculateLeaderboardService> _logger;

    public CalculateLeaderboardService(IServiceProvider serviceProvider,
        ILogger<CalculateLeaderboardService> logger)
    {
        _logger = logger;
        _scope = serviceProvider.CreateScope();
        var provider = _scope.ServiceProvider;
        _transactionService = provider.GetRequiredService<ITransactionService>();
        _createLeaderboardMemberService = provider.GetRequiredService<ICreateLeaderboardMemberService>();
        _deleteLeaderboardMemberService = provider.GetRequiredService<IDeleteLeaderboardMemberService>();
    }
    
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        foreach (int value in Enum.GetValues(typeof(LeagueTypes))) //TODO need to be refactored, cuz this method blocks app
        {
            if (value is 0 or 1)
                continue;

            await UpdateLeaderboardByLeague((LeagueTypes)value);
        }
        
        _timer = new Timer(async state => await UpdateLeaderboard(state),
            null,
            TimeSpan.Zero,
            TimeSpan.FromMinutes(7));
    }

    private async Task UpdateLeaderboard(object? state)
    {
        try
        {
            var currentLeague = LeagueConfiguration.GetNextLeagueLooped(_lastProcessedLeague);
            await UpdateLeaderboardByLeague(currentLeague);
            _lastProcessedLeague = currentLeague;
        }
        catch (Exception)
        {
            _logger.LogError($"Cannot calculate leaderboard for {_lastProcessedLeague}");
        }
    }

    private async Task UpdateLeaderboardByLeague(LeagueTypes currentLeague)
    {
        await _deleteLeaderboardMemberService.DeleteByLeagueTypeAsync(currentLeague); //TODO think about it, this method not part of .Commit()
        await _createLeaderboardMemberService.CreateByLeagueTypeAsync(currentLeague);
        await _transactionService.Commit();
        _transactionService.CleanChangeTracker();
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

    ~CalculateLeaderboardService()
    {
        Dispose(false);
    }
}