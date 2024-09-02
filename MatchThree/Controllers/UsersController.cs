using AutoMapper;
using MatchThree.API.Attributes;
using MatchThree.API.Models.User;
using MatchThree.Domain.Interfaces;
using MatchThree.Domain.Interfaces.User;
using MatchThree.Domain.Models;
using MatchThree.Shared.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MatchThree.API.Controllers;

[ApiController]
[Route("api/v1/users")]
public class UsersController(
    IMapper mapper,
    ICreateUserService createUserService,
    IUpdateUserService updateUserService,
    IDeleteUserService deleteUserService,
    IJwtTokenService jwtTokenService,
    ITransactionService transactionService)
{
    private readonly IMapper _mapper = mapper;
    private readonly ICreateUserService _createUserService = createUserService;
    private readonly IUpdateUserService _updateUserService = updateUserService;
    private readonly IDeleteUserService _deleteUserService = deleteUserService;
    private readonly IJwtTokenService _jwtTokenService = jwtTokenService;
    private readonly ITransactionService _transactionService = transactionService;

    /// <summary>
    /// User creation
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDto))]
    public async Task<IResult> Create([FromBody] UserCreateRequestDto request, 
        CancellationToken cancellationToken = new())
    {
        var entity = _mapper.Map<UserEntity>(request);
        var createdEntity = request.ReferrerId.HasValue 
            ? await _createUserService.CreateAsync(entity, request.ReferrerId.Value)
            : _createUserService.Create(entity);
        await _transactionService.Commit();
        return Results.Ok(_mapper.Map<UserDto>(createdEntity));
    }
    
    /// <summary>
    /// User auth
    /// </summary>
    [HttpPost("{userId:long}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
    public async Task<IResult> SignIn([FromMultiSource] UserSignInRequestDto request, 
        CancellationToken cancellationToken = new())
    {
        var entity = _mapper.Map<UserEntity>(request);
        await _updateUserService.SyncUserData(entity);
        var token = _jwtTokenService.GenerateJwtToken(entity.Id);
        await _transactionService.Commit();
        return Results.Ok(token);
    }

    /// <summary>
    /// User deletion
    /// </summary>
    [HttpDelete("{userId:long}")]
    [Authorize(Policy = AuthenticationConstants.UserIdPolicy)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    public async Task<IResult> Delete(long userId, CancellationToken cancellationToken = new())
    {
        //TODO The isDeleted flag should be added to avoid abuse of referrals 
        await _deleteUserService.DeleteAsync(userId);
        await _transactionService.Commit();
        return Results.NoContent();
    }
}