namespace MatchThree.API.Models.Users;

public class DebugUserRequestDto
{
    public long Id { get; init; }
    public string? Username { get; init; }
    public string FirstName { get; init; } = string.Empty;
    public bool IsPremium { get; init; }
    public long? ReferrerId { get; init; }
}