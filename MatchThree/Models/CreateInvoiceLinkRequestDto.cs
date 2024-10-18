using MatchThree.Shared.Enums;
using Microsoft.AspNetCore.Mvc;

namespace MatchThree.API.Models;

public class CreateInvoiceLinkRequestDto
{
    [FromQuery]
    public long UserId { get; init; }
    
    [FromQuery]
    public UpgradeTypes UpgradeType { get; init; }
}