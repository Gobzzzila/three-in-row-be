using MatchThree.Domain.Interfaces.LeaderboardMember;
using MatchThree.Repository.MSSQL;
using MatchThree.Repository.MSSQL.Models;
using MatchThree.Shared.Enums;
using Microsoft.EntityFrameworkCore;

namespace MatchThree.BL.Services.LeaderboardMember;

public class DeleteLeaderboardMemberService(MatchThreeDbContext context)
     : IDeleteLeaderboardMemberService
 {
     private readonly MatchThreeDbContext _context = context;

     public Task DeleteByLeagueTypeAsync(LeagueTypes league)
     {
         return _context.Set<LeaderboardMemberDbModel>()
             .Where(x => x.League == league)
             .ExecuteDeleteAsync();
     }
 }