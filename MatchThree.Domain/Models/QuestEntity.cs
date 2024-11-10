using MatchThree.Domain.Interfaces.Quests;
using MatchThree.Shared.Enums;

namespace MatchThree.Domain.Models;

public sealed class QuestEntity
{
    public Guid Id { get; init; }    
    public QuestTypes Type { get; init; }
    public string TittleKey { get; init; } = string.Empty;
    public string DescriptionKey { get; init; } = string.Empty;
    public uint Reward { get; init; }
    public string? ExternalLinkKey { get; init; } = string.Empty;
    public string? SecretCode { get; init; }
    public Func<IValidateQuestCompletionService, long, Task<bool>>? VerificationOfFulfillment { get; init; } 
}