using Microsoft.AspNetCore.Mvc;

namespace MatchThree.API.Models.User;

public class UserSignInRequestDto
{
    [FromRoute(Name = "userId")]
    public long Id { get; init; }
    
    [FromBody]
    public MainUserInfoDto MainUserInfo { get; init; }
}