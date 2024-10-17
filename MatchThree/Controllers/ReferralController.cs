using AutoMapper;
using MatchThree.API.Models;
using MatchThree.API.Models.Referrals;
using MatchThree.Domain.Interfaces.Referral;
using MatchThree.Shared.Constants;
using MatchThree.Shared.Enums;
using MatchThree.Shared.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Annotations;

namespace MatchThree.API.Controllers;

[ApiController]
[Route("api/v1")]
public class ReferralController(IMapper mapper,
    IReadReferralService readReferralService,
    IStringLocalizer<Localization> localization)
{
    private readonly IMapper _mapper = mapper;
    private readonly IReadReferralService _readReferralService = readReferralService;
    private readonly IStringLocalizer<Localization> _localization = localization;

    /// <summary>
    /// Get referrals by referrer identifier 
    /// </summary>
    [HttpGet("users/{userId:long}/referrals")]
    [Authorize(Policy = AuthenticationConstants.UserIdPolicy)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ReferralDto>))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
    [SwaggerOperation(OperationId = "GetReferrals", Tags = ["Referrals"])]
    public async Task<IResult> GetReferralsByReferrerId(long userId, CancellationToken cancellationToken = new())
    {
        var entity = await _readReferralService.GetReferralsByReferrerId(userId);
        return Results.Ok(_mapper.Map<List<ReferralDto>>(entity));
    }
    
    /// <summary>
    /// Get referrals by referrer identifier 
    /// </summary>
    [HttpGet("referrals/rewards")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ReferrerRewardDto))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
    [SwaggerOperation(OperationId = "GetRewardsInfo", Tags = ["Referrals"])]
    public Task<IResult> GetRewardsInfo(CancellationToken cancellationToken = new())
    {
        var result = new ReferrerRewardDto
        {
            RewardForInvitingRegularUser = ReferralConstants.RewardForInvitingRegularUser,
            RewardForInvitingPremiumUser = ReferralConstants.RewardForInvitingPremiumUser,
            AmountOfRewardsForIncreasingLevels = ReferralConstants.AmountOfRewardsForIncreasingLevels,
            RewardPerLeague = new()
            {
                new ReferrerRewardPerLeagueDto
                {
                    League = LeagueTypes.Crab,
                    LeagueName = _localization[LeagueTypes.Crab.GetTranslationId()!],
                    Reward = ReferralConstants.CrabLeagueReferrerReward
                },
                new ReferrerRewardPerLeagueDto
                {
                    League = LeagueTypes.Octopus,
                    LeagueName = _localization[LeagueTypes.Octopus.GetTranslationId()!],
                    Reward = ReferralConstants.OctopusLeagueReferrerReward
                },
                new ReferrerRewardPerLeagueDto
                {
                    League = LeagueTypes.Fish,
                    LeagueName = _localization[LeagueTypes.Fish.GetTranslationId()!],
                    Reward = ReferralConstants.FishLeagueReferrerReward
                },
                new ReferrerRewardPerLeagueDto
                {
                    League = LeagueTypes.Dolphin,
                    LeagueName = _localization[LeagueTypes.Dolphin.GetTranslationId()!],
                    Reward = ReferralConstants.DolphinLeagueReferrerReward
                },
                new ReferrerRewardPerLeagueDto
                {
                    League = LeagueTypes.Shark,
                    LeagueName = _localization[LeagueTypes.Shark.GetTranslationId()!],
                    Reward = ReferralConstants.SharkLeagueReferrerReward
                },
                new ReferrerRewardPerLeagueDto
                {
                    League = LeagueTypes.Whale,
                    LeagueName = _localization[LeagueTypes.Whale.GetTranslationId()!],
                    Reward = ReferralConstants.WhaleLeagueReferrerReward
                },
                new ReferrerRewardPerLeagueDto
                {
                    League = LeagueTypes.Humpback,
                    LeagueName = _localization[LeagueTypes.Humpback.GetTranslationId()!],
                    Reward = ReferralConstants.HumpbackLeagueReferrerReward
                }
            }
        };
        
        return Task.FromResult(Results.Ok(result));
    }
}