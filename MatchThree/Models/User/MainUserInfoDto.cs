namespace MatchThree.API.Models.User;

public class MainUserInfoDto
{
    public string? Username { get; init; }
    public string FirstName { get; init; } = string.Empty;
    public bool IsPremium { get; init; }
}