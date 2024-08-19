namespace MatchThree.Domain.Models;

public class UserEntity
{
    public long Id { get; set; }
    public string? Username { get; set; }
    public string? FirstName { get; set; }
    public bool IsPremium { get; set; }
    public DateTime CreatedAt{ get; set; }
    public DateTime LogoutAt{ get; set; }
}