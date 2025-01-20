using MatchThree.Domain.Interfaces.Referral;
using MatchThree.Domain.Interfaces.User;
using Microsoft.AspNetCore.Mvc;

namespace MatchThree.API.Controllers;

[ApiController]
[Route("api/v1/analytics")]
public class AnalyticController(IReadUserService readUserService, 
    IReadReferralService readReferralService)
{
    private readonly IReadUserService _readUserService = readUserService;
    private readonly IReadReferralService _readReferralService = readReferralService;

    /// <summary>
    /// Get raw analytics
    /// </summary>
    [HttpGet("get-raw")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IResult> GetRaw()
    {
        var usersAmount = await _readUserService.GetUsersAmountAsync();
        var analystReferrals = await _readReferralService.GetReferralAmountByReferrerIdAsync(496784594);
        
        return Results.Ok(new {UsersAmount = usersAmount, AnalystReferrals = analystReferrals});
    }
}