using MatchThree.Shared.Enums;
using Microsoft.AspNetCore.Mvc;

namespace MatchThree.API.Models;

public class CreateInvoiceLinkRequestDto
{
    [FromRoute(Name = "userId")]
    public long UserId { get; init; }
    
    [FromRoute(Name = "upgradeType")]
    public UpgradeTypes UpgradeType { get; init; }
}