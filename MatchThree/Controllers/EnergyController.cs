using AutoMapper;
using MatchThree.API.Models;
using MatchThree.Domain.Interfaces.Energy;
using Microsoft.AspNetCore.Mvc;

namespace MatchThree.API.Controllers;

[ApiController]
[Route("api/v1/users")]
public class EnergyController (IReadEnergyService energyReadService,
    IMapper mapper)
{
    /// <summary>
    /// Get energy by user identifier 
    /// </summary>
    [HttpGet("/{id:long}/energy")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EnergyDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    public async Task<IResult> GetById(long id, CancellationToken cancellationToken = new())
    {
        var entity = await energyReadService.GetByUserIdAsync(id);
        return Results.Ok(mapper.Map<EnergyDto>(entity));
    }
}