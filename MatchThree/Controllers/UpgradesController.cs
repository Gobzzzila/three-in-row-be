using AutoMapper;
using MatchThree.API.Models;
using MatchThree.Domain.Interfaces.Upgrades;
using MatchThree.Shared.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace MatchThree.API.Controllers;

[ApiController]
[Route("api/v1/users")]
public class UpgradesController(IMapper mapper, 
    IGetUpgradesService getUpgradesService,
    LinkGenerator linkGenerator)
{
    private readonly IMapper _mapper = mapper;
    private readonly IGetUpgradesService _getUpgradesService = getUpgradesService;
    private readonly LinkGenerator _linkGenerator = linkGenerator;

    /// <summary>
    /// Get list of upgrades
    /// </summary>
    [HttpGet("{userId:long}/upgrades")]
    [Authorize(Policy = AuthenticationConstants.UserIdPolicy)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<UpgradeDto>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
    [SwaggerOperation(OperationId = "GetUpgrades", Tags = ["Upgrades"])]
    public async Task<IResult> GetAllUpgrades(long userId, CancellationToken cancellationToken = new())
    {
        var entities = await _getUpgradesService.GetAll(userId);
        var result = new List<UpgradeDto>(entities.Count);
        var values = new { userId = userId.ToString() };
        foreach (var entity in entities)
        {
            var upgradeDto = _mapper.Map<UpgradeDto>(entity);
            var executePath = _linkGenerator.GetPathByName(entity.ExecutePathName, values); //TODO can be done im mapper resolver
            upgradeDto.ExecutePath = executePath!;
            result.Add(upgradeDto);
        }
        return Results.Ok(result);
    }
}
