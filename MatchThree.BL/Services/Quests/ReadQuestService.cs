using MatchThree.BL.Configuration;
using MatchThree.Domain.Interfaces.Quests;
using MatchThree.Domain.Models;
using MatchThree.Repository.MSSQL;
using MatchThree.Repository.MSSQL.Models;
using MatchThree.Shared.Exceptions;

namespace MatchThree.BL.Services.Quests;

public class ReadQuestService(MatchThreeDbContext context) 
    : IReadQuestService
{
    private readonly MatchThreeDbContext _context = context;
    
    public async Task<List<QuestEntity>> GetUncompleted(long userId)
    {
        var dbModel = await _context.Set<CompletedQuestsDbModel>().FindAsync(userId);
        if (dbModel is null)
            throw new NoDataFoundException();

        return QuestsConfiguration.GetUncompleted(dbModel.QuestIds);
    }

    public async Task<List<QuestEntity>> GetCompleted(long userId)
    {
        var dbModel = await _context.Set<CompletedQuestsDbModel>().FindAsync(userId);
        if (dbModel is null)
            throw new NoDataFoundException();

        return QuestsConfiguration.GetCompleted(dbModel.QuestIds);
    }
}