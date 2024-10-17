using MatchThree.Shared.Enums;

namespace MatchThree.Domain.Models.Upgrades;

public class UpgradeEntity
{
    public string DescriptionTextKey { get; set; } = string.Empty;
    public string? BlockingTextKey { get; set; }
    public object?[] BlockingTextArgs { get; set; } = [];
    public int CurrentLevel { get; set; }
    public uint? Price { get; set; }
    public bool IsStars { get; set; }
    public UpgradeTypes Type { get; set; }
    public string ExecutePathName { get; set; } = string.Empty;
}