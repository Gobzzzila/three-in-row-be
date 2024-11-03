using MatchThree.Domain.Interfaces.Quests;
using MatchThree.Repository.MSSQL;
using MatchThree.Repository.MSSQL.Models;

namespace MatchThree.BL.Services.Quests;

public sealed class CreateCompletedQuestsService(MatchThreeDbContext context) 
    : ICreateCompletedQuestsService
{
    private readonly MatchThreeDbContext _context = context;

    public void Create(long userId)
    {
        _context.Set<CompletedQuestsDbModel>().Add(new CompletedQuestsDbModel
        {
            Id = userId,
            QuestIds = []
        });
    }
}