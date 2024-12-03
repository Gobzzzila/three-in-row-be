namespace MatchThree.API.Models;

public class QuestDto
{
    public Guid Id { get; init; }
    public string Tittle { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public string PictureName { get; init; } = string.Empty;
    public uint Reward { get; init; }
    public bool IsWithSecretCode { get; init; }
    public string? ActionLink { get; init; } = string.Empty;
    public bool? IsLinkInternal { get; init; }
}