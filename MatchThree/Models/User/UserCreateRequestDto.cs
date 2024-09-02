namespace MatchThree.API.Models.User;

public class UserCreateRequestDto
{
    public long Id { get; init; }
    public long? ReferrerId { get; init; }
    public string? Username { get; init; }
    public string FirstName { get; init; } = string.Empty;
    public bool IsPremium { get; init; }
}