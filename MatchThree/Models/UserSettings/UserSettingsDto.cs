namespace MatchThree.API.Models.UserSettings;

public class UserSettingsDto
{
    public long Id { get; set; }
    public bool Notifications { get; set; }
    public string Culture { get; set; } = string.Empty;
}