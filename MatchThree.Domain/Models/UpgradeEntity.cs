using MatchThree.Shared.Enums;

namespace MatchThree.Domain.Models;

public class UpgradeEntity
{
    public string HeaderTextKey { get; set; } = string.Empty;
    public string DescriptionTextKey { get; set; } = string.Empty;
    public string? BlockingTextKey { get; set; }
    public int CurrentLevel { get; set; }
    public uint? Price { get; set; }
    public bool IsStars { get; set; }
    public UpgradeCategories Category { get; set; }
    public string ExecutePathName { get; set; } = string.Empty;
}