using AutoMapper;
using MatchThree.API.Models;
using MatchThree.Domain.Interfaces.Balance;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MatchThree.API.Controllers;

[ApiController]
[Route("api/v1/users")]
public class BalanceController(IMapper mapper,
    IReadBalanceService readBalanceService)
{
    private readonly IMapper _mapper = mapper;
    private readonly IReadBalanceService _readBalanceService = readBalanceService;
    
    /// <summary>
    /// Get balance by user identifier 
    /// </summary>
    [HttpGet("{id:long}/balance")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BalanceDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    public async Task<IResult> GetById(long id, CancellationToken cancellationToken = new())
    {
        var entity = await _readBalanceService.GetByIdAsync(id);
        return Results.Ok(_mapper.Map<BalanceDto>(entity));
    }
}