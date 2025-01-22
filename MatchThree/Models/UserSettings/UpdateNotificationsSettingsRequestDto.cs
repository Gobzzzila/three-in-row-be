using Microsoft.AspNetCore.Mvc;

namespace MatchThree.API.Models.UserSettings;

public class UpdateNotificationsSettingsRequestDto
{
    [FromRoute(Name = "userId")]
    public long UserId { get; init; }

    [FromQuery(Name = "notifications")] 
    public bool Notifications { get; init; }
}