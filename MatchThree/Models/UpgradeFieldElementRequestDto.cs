using MatchThree.Shared.Enums;
using Microsoft.AspNetCore.Mvc;

namespace MatchThree.API.Models;

public class UpgradeFieldElementRequestDto
{
    [FromRoute(Name = "userId")]
    public long UserId { get; init; }
    
    [FromRoute(Name = "fieldElement")]
    public CryptoTypes CryptoType { get; init; }
}