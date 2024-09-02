using Microsoft.AspNetCore.Mvc;

namespace MatchThree.API.Models.User;

public class UserSignInRequestDto
{
    [FromRoute(Name = "userId")]
    public long Id { get; init; }
    [FromQuery]
    public string? Username { get; init; }
    [FromQuery]
    public string? FirstName { get; init; }
    [FromQuery]
    public bool IsPremium { get; init; }
}