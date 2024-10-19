using AutoMapper;
using MatchThree.API.Attributes;
using MatchThree.API.Models;
using MatchThree.API.Models.Upgrades;
using MatchThree.Domain.Interfaces;
using MatchThree.Domain.Interfaces.Upgrades;
using MatchThree.Shared.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace MatchThree.API.Controllers;

[ApiController]
[Route("api/v1/users")]
public class UpgradesController(IMapper mapper, 
    ITelegramBotService telegramBotService,
    IGetUpgradesService getUpgradesService)
{
    private readonly IMapper _mapper = mapper;
    private readonly ITelegramBotService _telegramBotService = telegramBotService;
    private readonly IGetUpgradesService _getUpgradesService = getUpgradesService;

    /// <summary>
    /// Get list of upgrades
    /// </summary>
    [HttpGet("{userId:long}/upgrades")]
    [Authorize(Policy = AuthenticationConstants.UserIdPolicy)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GroupedUpgradesDto>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
    [SwaggerOperation(OperationId = "GetUpgrades", Tags = ["Upgrades"])]
    public async Task<IResult> GetAllUpgrades(long userId, CancellationToken cancellationToken = new())
    {
        var groupedUpgradesEntities = await _getUpgradesService.GetAll(userId);
        return Results.Ok(_mapper.Map<List<GroupedUpgradesDto>>(groupedUpgradesEntities));
    }
    
    /// <summary>
    /// User creation
    /// </summary>
    [HttpPost("{userId:long}/create-invoice/{upgradeType:int}", Name = EndpointsConstants.CreateInvoiceLinkEndpoint)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
    [SwaggerOperation(OperationId = "CreateInvoiceLink", Tags = ["Upgrades"])]
    public async Task<IResult> Create([FromMultiSource]CreateInvoiceLinkRequestDto request, 
        CancellationToken cancellationToken = new())
    {
        var invoiceLink = await _telegramBotService.CreateInvoiceLink(new()
        {
            UserId = request.UserId,
            UpgradeType = request.UpgradeType
        });
        
        return Results.Ok(invoiceLink);
    }
}
