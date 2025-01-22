using MatchThree.Shared.Enums;

namespace MatchThree.API.Models;

public class UserSettingsDto
{
    public long Id { get; set; }
    public bool Notifications { get; set; }
    public string Culture { get; set; } = string.Empty;
}