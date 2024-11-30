using MatchThree.BL.Configuration;
using MatchThree.Domain.Interfaces.Balance;
using MatchThree.Domain.Interfaces.Quests;
using MatchThree.Repository.MSSQL;
using MatchThree.Repository.MSSQL.Models;
using MatchThree.Shared.Constants;
using MatchThree.Shared.Exceptions;

namespace MatchThree.BL.Services.Quests;

public class CompleteQuestService(MatchThreeDbContext context,
    IValidateQuestCompletionService validateQuestCompletionService,
    IUpdateBalanceService updateBalanceService) 
    : ICompleteQuestService
{
    private readonly MatchThreeDbContext _context = context;
    private readonly IValidateQuestCompletionService _validateQuestCompletionService = validateQuestCompletionService;
    private readonly IUpdateBalanceService _updateBalanceService = updateBalanceService;

    public async Task CompleteQuest(long userId, Guid questId, string? secretCode)
    {
        var dbModel = await _context.Set<CompletedQuestsDbModel>().FindAsync(userId);
        
        if (dbModel is null)
            throw new NoDataFoundException();

        if (dbModel.QuestIds.Contains(questId))
            throw new ValidationException(TranslationConstants.ExceptionQuestAlreadyCompletedTextKey);
        
        var questEntity = QuestsConfiguration.GetById(questId);
        if (questEntity.IsDeleted)
            throw new ValidationException(TranslationConstants.ExceptionQuestOutdatedTextKey);
        
        var isCompleted = true;
        if (questEntity.VerificationOfFulfillment is not null)
            isCompleted = await questEntity.VerificationOfFulfillment.Invoke(_validateQuestCompletionService, userId);
        
        if (questEntity.SecretCode is not null)
            isCompleted = questEntity.SecretCode.Equals(secretCode, StringComparison.OrdinalIgnoreCase);
        
        if (!isCompleted)
            throw new ValidationException(TranslationConstants.ExceptionQuestsConditionNotMetTextKey);

        await _updateBalanceService.AddBalanceAsync(userId, questEntity.Reward);
        dbModel.QuestIds.Add(questId);
        _context.Set<CompletedQuestsDbModel>().Update(dbModel);
    }
}