using MatchThree.Domain.Interfaces.Quests;
using MatchThree.Repository.MSSQL;
using MatchThree.Repository.MSSQL.Models;
using MatchThree.Shared.Exceptions;

namespace MatchThree.BL.Services.Quests;

public sealed class DeleteCompletedQuestsService(MatchThreeDbContext context) 
    : IDeleteCompletedQuestsService
{
    private readonly MatchThreeDbContext _context = context;

    public async Task DeleteByUserIdAsync(long userId)
    {
        var dbModel = await _context.Set<CompletedQuestsDbModel>().FindAsync(userId);
        if (dbModel is null)
            throw new NoDataFoundException();
        
        _context.Set<CompletedQuestsDbModel>().Remove(dbModel);
    }
}