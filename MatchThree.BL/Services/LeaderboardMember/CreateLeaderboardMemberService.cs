using MatchThree.Domain.Interfaces.LeaderboardMember;
using MatchThree.Domain.Models.Configuration;
using MatchThree.Repository.MSSQL;
using MatchThree.Repository.MSSQL.Models;
using MatchThree.Shared.Enums;
using Microsoft.EntityFrameworkCore;

namespace MatchThree.BL.Services.LeaderboardMember;

public class CreateLeaderboardMemberService(MatchThreeDbContext context)
    : ICreateLeaderboardMemberService
{
    public async Task CreateByLeagueTypeAsync(LeagueTypes league)
    {
        var leagueParam = LeagueConfiguration.GetParamsByType(league);

        var dbModelsToAdd = await context.Set<BalanceDbModel>()
            .AsNoTracking()
            .Include(x => x.User)
            .Where(x => x.OverallBalance > leagueParam.MinValue && x.OverallBalance <= leagueParam.MaxValue)
            .OrderByDescending(x => x.OverallBalance)
            .Take(1000)
            .Select(x => new LeaderboardMemberDbModel
            {
                Id = x.User!.Id,
                FirstName = x.User!.FirstName,
                League = league,
                OverallBalance = x.OverallBalance
            })
            .ToListAsync();
        
        if (dbModelsToAdd.Count == 0)
            return;

        var i = 1;
        foreach (var dbModel in dbModelsToAdd)
        {
            dbModel.TopSpot = i;
            i++;
        }
        
        await context.Set<LeaderboardMemberDbModel>().AddRangeAsync(dbModelsToAdd);
    }
}