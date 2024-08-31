using AutoMapper;
using MatchThree.API.Models.User;
using MatchThree.Domain.Interfaces;
using MatchThree.Domain.Interfaces.User;
using MatchThree.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace MatchThree.API.Controllers;

[ApiController]
[Route("api/v1/users")]
public class UsersController(
    IMapper mapper,
    ICreateUserService createUserService,
    IDeleteUserService deleteUserService,
    ITransactionService transactionService)
{
    /// <summary>
    /// User creation
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDto))]
    public async Task<IResult> Create([FromBody] UserCreateDto createDto, 
        CancellationToken cancellationToken = new())
    {
        var entity = mapper.Map<UserEntity>(createDto);
        var createdEntity = createDto.ReferrerId.HasValue 
            ? await createUserService.CreateAsync(entity, createDto.ReferrerId.Value)
            : createUserService.Create(entity);
        await transactionService.Commit();
        return Results.Ok(mapper.Map<UserDto>(createdEntity));
    }

    /// <summary>
    /// User deletion
    /// </summary>
    [HttpDelete("/{id:long}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    public async Task<IResult> Delete(long id, CancellationToken cancellationToken = new())
    {
        //TODO The isDeleted flag should be added to avoid abuse of referrals 
        await deleteUserService.DeleteAsync(id);
        await transactionService.Commit();
        return Results.NoContent();
    }
}