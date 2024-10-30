using AutoMapper;
using MatchThree.API.Models;
using MatchThree.Domain.Interfaces.FieldElement;
using MatchThree.Shared.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace MatchThree.API.Controllers;

[ApiController]
[Route("api/v1/field-elements")]
public class FieldElementController(IReadFieldElementService readFieldElementService,
    IMapper mapper)
{
    private readonly IReadFieldElementService _readFieldElementService = readFieldElementService;
    private readonly IMapper _mapper = mapper;

    /// <summary>
    /// Gets field elements by user id
    /// </summary>
    [HttpGet("{userId:long}")]
    [Authorize(Policy = AuthenticationConstants.UserIdPolicy)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<FieldElementDto>))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
    [SwaggerOperation(OperationId = "GetFieldElements", Tags = ["FieldElements"])]
    public async Task<IResult> GetFieldElements(long userId, CancellationToken cancellationToken = new())
    {
        var fieldElements = await _readFieldElementService.GetByUserIdAsync(userId);
        return Results.Ok(_mapper.Map<List<FieldElementDto>>(fieldElements));
    }
}