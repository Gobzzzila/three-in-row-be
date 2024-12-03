using MatchThree.Domain.Interfaces.Quests;
using MatchThree.Shared.Enums;

namespace MatchThree.Domain.Models;

public sealed class QuestEntity
{
    public Guid Id { get; init; }    
    public QuestTypes Type { get; init; }
    public string TittleKey { get; init; } = string.Empty;
    public string DescriptionKey { get; init; } = string.Empty;
    public string PictureName { get; init; } = string.Empty;
    public uint Reward { get; init; }
    public string? ActionLinkKey { get; init; }
    public bool? IsLinkInternal { get; init; }
    public string? SecretCode { get; init; }
    public Func<IValidateQuestCompletionService, long, Task<bool>>? VerificationOfFulfillment { get; init; } 
    public bool IsСompressible { get; init; }
    public bool IsDeleted { get; init; }
}