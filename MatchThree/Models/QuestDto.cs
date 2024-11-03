using MatchThree.Shared.Enums;

namespace MatchThree.API.Models;

public class QuestDto
{
    public Guid Id { get; init; }    
    public QuestTypes Type { get; init; }
    public string Tittle { get; init; } = string.Empty;
    public string? Description { get; init; }
    public uint Reward { get; init; }
    public string? ExecuteLink { get; init; } = string.Empty;
}