using MatchThree.Domain.Interfaces.LeaderboardMember;
using MatchThree.Repository.MSSQL;
using MatchThree.Shared.Enums;
using Microsoft.EntityFrameworkCore;

namespace MatchThree.API.Services;

public class CalculateLeaderboardService : IHostedService, IDisposable
{
    private Timer? _timer;
    private bool _disposed;

    private readonly IServiceScope _scope;
    private readonly MatchThreeDbContext _context;
    private readonly ICreateLeaderboardMemberService _createLeaderboardMemberService;
    private readonly IDeleteLeaderboardMemberService _deleteLeaderboardMemberService;
    private readonly ILogger<CalculateLeaderboardService> _logger;

    public CalculateLeaderboardService(IServiceProvider serviceProvider,
        ILogger<CalculateLeaderboardService> logger)
    {
        _logger = logger;
        _scope = serviceProvider.CreateScope();
        var provider = _scope.ServiceProvider;
        _context = provider.GetRequiredService<MatchThreeDbContext>();
        _createLeaderboardMemberService = provider.GetRequiredService<ICreateLeaderboardMemberService>();
        _deleteLeaderboardMemberService = provider.GetRequiredService<IDeleteLeaderboardMemberService>();
    }
    
    public Task StartAsync(CancellationToken cancellationToken)
    {
        _timer = new Timer(async state => await UpdateLeaderboard(state),
            null,
            TimeSpan.Zero,
            TimeSpan.FromMinutes(5));   //TODO Move magic number to appsettings
        
        return Task.CompletedTask;
    }

    private async Task UpdateLeaderboard(object? state)
    {
        await _context.Database.CreateExecutionStrategy().ExecuteAsync(CalculateLeaderboardInTransaction);
        _context.ChangeTracker.Clear();  
    }

    private async Task CalculateLeaderboardInTransaction()
    {
        await using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            await _deleteLeaderboardMemberService.DeleteAll();
            foreach (var value in Enum.GetValues<LeagueTypes>())
            {
                if (value is LeagueTypes.Undefined)
                    continue;

                await _createLeaderboardMemberService.CreateByLeagueTypeAsync(value);
            }
            
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Cannot calculate leaderboard: {ex}");
            await transaction.RollbackAsync();
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

    ~CalculateLeaderboardService()
    {
        Dispose(false);
    }
}