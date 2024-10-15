using Microsoft.AspNetCore.Mvc;

namespace MatchThree.API.Models.Users;

public class UserRequestDto
{
    [FromBody]
    public required string InitData { get; init; }
    
    [FromQuery]
    public long? ReferrerId { get; init; }
}