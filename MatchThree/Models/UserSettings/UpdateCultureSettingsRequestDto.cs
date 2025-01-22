using Microsoft.AspNetCore.Mvc;

namespace MatchThree.API.Models.UserSettings;

public class UpdateCultureSettingsRequestDto
{
    [FromRoute(Name = "userId")]
    public long UserId { get; init; }

    [FromQuery(Name = "culture")] public string Culture { get; init; } = string.Empty;
}