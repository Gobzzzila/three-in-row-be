using Microsoft.AspNetCore.Mvc;

namespace MatchThree.API.Models;

public sealed class MakeMoveRequestDto
{
    [FromRoute(Name = "userId")]
    public long UserId { get; init; }
    
    [FromBody]
    public MoveInfoRequestDto Body { get; init; }
}