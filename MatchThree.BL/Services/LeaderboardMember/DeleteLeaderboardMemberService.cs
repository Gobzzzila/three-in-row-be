using MatchThree.Domain.Interfaces.LeaderboardMember;
using MatchThree.Repository.MSSQL;
using MatchThree.Repository.MSSQL.Models;
using Microsoft.EntityFrameworkCore;

namespace MatchThree.BL.Services.LeaderboardMember;

public class DeleteLeaderboardMemberService(MatchThreeDbContext context)
     : IDeleteLeaderboardMemberService
 {
     private readonly MatchThreeDbContext _context = context;
     
     public Task ExecuteDeleteAllAsync()
     {
         return _context.Set<LeaderboardMemberDbModel>()
             .ExecuteDeleteAsync();
     }
 }