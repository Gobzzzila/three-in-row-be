using MatchThree.Domain.Interfaces.LeaderboardMember;
using MatchThree.Repository.MSSQL;
using MatchThree.Shared.Constants;
using Microsoft.EntityFrameworkCore;

namespace MatchThree.BL.Services.LeaderboardMember;

public class DeleteLeaderboardMemberService(MatchThreeDbContext context)
     : IDeleteLeaderboardMemberService
 {
     private readonly MatchThreeDbContext _context = context;
     
     public Task ExecuteDeleteAllAsync()
     { 
#pragma warning disable EF1002                  
//All the args from app, so it cannot be sql-injection 
         return _context.Database.ExecuteSqlRawAsync($"DELETE FROM {LeaderBoardConstants.LeaderBoardTableName};");
#pragma warning restore EF1002
     }
 }