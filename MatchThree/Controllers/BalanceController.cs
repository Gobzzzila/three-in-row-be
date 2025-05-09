﻿using AutoMapper;
using MatchThree.API.Models;
using MatchThree.Domain.Interfaces.Balance;
using MatchThree.Shared.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

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
    [HttpGet("{userId:long}/balance")]
    [Authorize(Policy = AuthenticationConstants.UserIdPolicy)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BalanceDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
    [SwaggerOperation(OperationId = "GetBalance", Tags = ["Balance"])]
    public async Task<IResult> GetById(long userId, CancellationToken cancellationToken = new())
    {
        var entity = await _readBalanceService.GetByIdAsync(userId);
        return Results.Ok(_mapper.Map<BalanceDto>(entity));
    }
}