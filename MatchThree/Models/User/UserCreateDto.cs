namespace MatchThree.API.Models.User;

public class UserCreateDto
{
    public long Id { get; set; }
    public long? ReferrerId { get; set; }
    public string? Username { get; set; }
    public string? FirstName { get; set; }
    public bool IsPremium { get; set; }
}