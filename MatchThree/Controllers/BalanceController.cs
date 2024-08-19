using AutoMapper;
using MatchThree.API.Models;
using MatchThree.Domain.Interfaces.Balance;
using Microsoft.AspNetCore.Mvc;

namespace MatchThree.API.Controllers;

[ApiController]
[Route("api/v1/users")]
public class BalanceController(IMapper mapper,
    IReadBalanceService readBalanceService)
{
    /// <summary>
    /// Get balance by user identifier 
    /// </summary>
    [HttpGet("/{id:long}/balance")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BalanceDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    public async Task<IResult> GetById(long id, CancellationToken cancellationToken = new())
    {
        var entity = await readBalanceService.GetByIdAsync(id);
        return Results.Ok(mapper.Map<BalanceDto>(entity));
    }
}