using AutoMapper;
using MatchThree.API.Models;
using MatchThree.Domain.Interfaces;
using MatchThree.Domain.Interfaces.Energy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MatchThree.API.Controllers;

[ApiController]
[Route("api/v1/user")]
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
    [HttpGet("{id:long}/energy")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EnergyDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    public async Task<IResult> GetById(long id, CancellationToken cancellationToken = new())
    {
        var entity = await _energyReadService.GetByUserIdAsync(id);
        return Results.Ok(_mapper.Map<EnergyDto>(entity));
    }
    
    /// <summary>
    /// Upgrade reserve by user identifier 
    /// </summary>
    [HttpPost("{id:long}/upgrade-reserve")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status402PaymentRequired, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(ProblemDetails))]
    public async Task<IResult> UpgradeReserve(long id, CancellationToken cancellationToken = new())
    {
        await _updateEnergyService.UpgradeReserveAsync(id);
        await _transactionService.Commit();
        return Results.Ok();
    }
    
    /// <summary>
    /// Upgrade reserve by user identifier 
    /// </summary>
    [HttpPost("{id:long}/upgrade-recovery")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status402PaymentRequired, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(ProblemDetails))]
    public async Task<IResult> UpgradeRecovery(long id, CancellationToken cancellationToken = new())
    {
        await _updateEnergyService.UpgradeRecoveryAsync(id);
        await _transactionService.Commit();
        return Results.Ok();
    }
    
    /// <summary>
    /// Use an energy drink
    /// </summary>
    [HttpPost("{id:long}/energy-drink")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EnergyDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status402PaymentRequired, Type = typeof(ProblemDetails))]
    public async Task<IResult> UseEnergyDrink(long id, CancellationToken cancellationToken = new())
    {
        var entity = await _updateEnergyService.UseEnergyDrinkAsync(id);
        await _transactionService.Commit();
        return Results.Ok(_mapper.Map<EnergyDto>(entity));
    }
    
    /// <summary>
    /// Purchase an energy drink
    /// </summary>
    [HttpPost("{id:long}/purchase-energy-drink")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status402PaymentRequired, Type = typeof(ProblemDetails))]
    public async Task<IResult> PurchaseEnergyDrink(long id, CancellationToken cancellationToken = new())
    {
        await _updateEnergyService.PurchaseEnergyDrinkAsync(id);
        await _transactionService.Commit();
        return Results.Ok();
    }
}