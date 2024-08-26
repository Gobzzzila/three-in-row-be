using AutoMapper;
using MatchThree.API.Models;
using MatchThree.Domain.Interfaces;
using MatchThree.Domain.Interfaces.Energy;
using Microsoft.AspNetCore.Mvc;

namespace MatchThree.API.Controllers;

[ApiController]
[Route("api/v1/energies")]
public class EnergyController (IReadEnergyService energyReadService,
    IUpdateEnergyService updateEnergyService,
    ITransactionService transactionService,
    IMapper mapper)
{
    /// <summary>
    /// Get energy by user identifier 
    /// </summary>
    [HttpGet("{id:long}/")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EnergyDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    public async Task<IResult> GetById(long id, CancellationToken cancellationToken = new())
    {
        var entity = await energyReadService.GetByUserIdAsync(id);
        return Results.Ok(mapper.Map<EnergyDto>(entity));
    }
    
    /// <summary>
    /// Upgrade reserve by user identifier 
    /// </summary>
    [HttpPost("{id:long}/upgrade-reserve")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status402PaymentRequired, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(ProblemDetails))]
    public async Task<IResult> UpgradeReserve(long id, CancellationToken cancellationToken = new())
    {
        await updateEnergyService.UpgradeReserveAsync(id);
        await transactionService.Commit();
        return Results.Ok();
    }
    
    /// <summary>
    /// Upgrade reserve by user identifier 
    /// </summary>
    [HttpPost("{id:long}/upgrade-recovery")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status402PaymentRequired, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(ProblemDetails))]
    public async Task<IResult> UpgradeRecovery(long id, CancellationToken cancellationToken = new())
    {
        await updateEnergyService.UpgradeRecoveryAsync(id);
        await transactionService.Commit();
        return Results.Ok();
    }
    
    /// <summary>
    /// Use an energy drink
    /// </summary>
    [HttpPost("{id:long}/energy-drink")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EnergyDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status402PaymentRequired, Type = typeof(ProblemDetails))]
    public async Task<IResult> UseEnergyDrink(long id, CancellationToken cancellationToken = new())
    {
        var entity = await updateEnergyService.UseEnergyDrinkAsync(id);
        await transactionService.Commit();
        return Results.Ok(mapper.Map<EnergyDto>(entity));
    }
    
    /// <summary>
    /// Purchase an energy drink
    /// </summary>
    [HttpPost("{id:long}/purchase-energy-drink")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status402PaymentRequired, Type = typeof(ProblemDetails))]
    public async Task<IResult> PurchaseEnergyDrink(long id, CancellationToken cancellationToken = new())
    {
        await updateEnergyService.PurchaseEnergyDrinkAsync(id);
        await transactionService.Commit();
        return Results.Ok();
    }
}