﻿using AutoMapper;
using MatchThree.API.Models;
using MatchThree.Domain.Interfaces;
using MatchThree.Domain.Interfaces.Energy;
using MatchThree.Shared.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace MatchThree.API.Controllers;

[ApiController]
[Route("api/v1/users")]
public class EnergyController (IReadEnergyService energyReadService,
    IUpdateEnergyService updateEnergyService,
    ITransactionService transactionService,
    IMapper mapper)
{
    private readonly IReadEnergyService _energyReadService = energyReadService;
    private readonly IUpdateEnergyService _updateEnergyService = updateEnergyService;
    private readonly ITransactionService _transactionService = transactionService;
    private readonly IMapper _mapper = mapper;
    
    /// <summary>
    /// Get energy by user identifier 
    /// </summary>
    [HttpGet("{userId:long}/energy")]
    [Authorize(Policy = AuthenticationConstants.UserIdPolicy)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EnergyDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
    [SwaggerOperation(OperationId = "GetEnergy", Tags = ["Energy"])]
    public async Task<IResult> GetById(long userId, CancellationToken cancellationToken = new())
    {
        var entity = await _energyReadService.GetByUserIdAsync(userId);
        return Results.Ok(_mapper.Map<EnergyDto>(entity));
    }
    
    /// <summary>
    /// Upgrade reserve by user identifier 
    /// </summary>
    [HttpPost("{userId:long}/upgrade-reserve", Name = EndpointsConstants.UpgradeEnergyReserveEndpointName)]
    [Authorize(Policy = AuthenticationConstants.UserIdPolicy)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status402PaymentRequired, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
    [SwaggerOperation(OperationId = "UpgradeReserve", Tags = ["Energy"])]
    public async Task<IResult> UpgradeReserve(long userId, CancellationToken cancellationToken = new())
    {
        await _updateEnergyService.UpgradeReserveAsync(userId);
        await _transactionService.CommitAsync();
        return Results.NoContent();
    }
    
    /// <summary>
    /// Upgrade reserve by user identifier 
    /// </summary>
    [HttpPost("{userId:long}/upgrade-recovery", Name = EndpointsConstants.UpgradeEnergyRecoveryEndpointName)]
    [Authorize(Policy = AuthenticationConstants.UserIdPolicy)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status402PaymentRequired, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
    [SwaggerOperation(OperationId = "UpgradeRecovery", Tags = ["Energy"])]
    public async Task<IResult> UpgradeRecovery(long userId, CancellationToken cancellationToken = new())
    {
        await _updateEnergyService.UpgradeRecoveryAsync(userId);
        await _transactionService.CommitAsync();
        return Results.NoContent();
    }
    
    /// <summary>
    /// Use an energy drink
    /// </summary>
    [HttpPost("{userId:long}/energy-drink", Name = EndpointsConstants.UseEnergyDrinkEndpoint)]
    [Authorize(Policy = AuthenticationConstants.UserIdPolicy)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status402PaymentRequired, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
    [SwaggerOperation(OperationId = "UseEnergyDrink", Tags = ["Energy"])]
    public async Task<IResult> UseEnergyDrink(long userId, CancellationToken cancellationToken = new())
    {
        await _updateEnergyService.UseEnergyDrinkAsync(userId);
        await _transactionService.CommitAsync();
        return Results.NoContent();
    }
}