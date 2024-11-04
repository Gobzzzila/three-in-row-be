using Microsoft.AspNetCore.Mvc;

namespace MatchThree.API.Models;

public class CompleteQuestRequestDto
{
    [FromRoute(Name = "userId")]
    public long UserId { get; init; }
    
    [FromRoute(Name = "questId")]
    public Guid QuestId { get; init; }
    
    [FromQuery(Name = "secretCode")]
    public string? SecretCode { get; init; }
}