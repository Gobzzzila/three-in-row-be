using MatchThree.Shared.Enums;

namespace MatchThree.Domain.Models.Upgrades;

public class UpgradeEntity
{
    public object?[] HeaderTextArgs { get; set; } = [];
    public object?[] DescriptionTextArgs { get; set; } = [];
    public object?[] BlockingTextArgs { get; set; } = [];
    public bool IsBlocked { get; set; }
    public int CurrentLevel { get; set; }
    public uint? Price { get; set; }
    public bool IsStars { get; set; }
    public UpgradeTypes Type { get; set; }
    public string ExecutePathName { get; set; } = string.Empty;
    public object? ExecutePathArgs { get; set; }
}