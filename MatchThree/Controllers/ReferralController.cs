using AutoMapper;
using MatchThree.API.Models;
using MatchThree.Domain.Interfaces.Referral;
using MatchThree.Shared.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MatchThree.API.Controllers;

[ApiController]
[Route("api/v1/users")]
public class ReferralController(IMapper mapper,
    IReadReferralService readReferralService)
{
    private readonly IMapper _mapper = mapper;
    private readonly IReadReferralService _readReferralService = readReferralService;

    /// <summary>
    /// Get referrals by referrer identifier 
    /// </summary>
    [HttpGet("{userId:long}/referrals")]
    [Authorize(Policy = AuthenticationConstants.UserIdPolicy)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ReferralDto>))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
    public async Task<IResult> GetReferralsByReferrerId(long userId, CancellationToken cancellationToken = new())
    {
        var entity = await _readReferralService.GetReferralsByReferrerId(userId);
        return Results.Ok(_mapper.Map<List<ReferralDto>>(entity));
    }
}