using AutoMapper;
using MatchThree.API.Authentication.Interfaces;
using MatchThree.API.Models.Users;
using MatchThree.Domain.Interfaces;
using MatchThree.Domain.Interfaces.User;
using MatchThree.Domain.Models;
using MatchThree.Shared.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

#if DEBUG

namespace MatchThree.API.Controllers;

[ApiController]
[Route("api/v1/debug")]
public class DebugController(IMapper mapper,
    IReadUserService readUserService,
    ICreateUserService createUserService,
    IUpdateUserService updateUserService,
    IDeleteUserService deleteUserService,
    IJwtTokenService jwtTokenService,
    ITransactionService transactionService)
{
    private readonly IMapper _mapper = mapper;
    private readonly IReadUserService _readUserService = readUserService;
    private readonly ICreateUserService _createUserService = createUserService;
    private readonly IUpdateUserService _updateUserService = updateUserService;
    private readonly IDeleteUserService _deleteUserService = deleteUserService;
    private readonly IJwtTokenService _jwtTokenService = jwtTokenService;
    private readonly ITransactionService _transactionService = transactionService;
    
    /// <summary>
    /// User creation
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
    [SwaggerOperation(OperationId = "SignUpUser", Tags = ["Debug"])]
    public async Task<IResult> Create([FromBody] DebugUserRequestDto request, 
        CancellationToken cancellationToken = new())
    {
        var userEntity = await _readUserService.FindByIdAsync(request.Id);

        if (userEntity is null)
        {
            var creationEntity = _mapper.Map<UserEntity>(request);
            if (request.ReferrerId.HasValue)
                await _createUserService.CreateWithReferrerAsync(creationEntity, request.ReferrerId.Value);
            else
                _createUserService.Create(creationEntity);
        }
        else
        {
            var entity = _mapper.Map<UserEntity>(request);
            await _updateUserService.SyncUserDataAsync(entity);
        }
        
        var token = _jwtTokenService.GenerateJwtToken(request.Id);
        await _transactionService.CommitAsync();
        return Results.Ok(token);
    }
    
    /// <summary>
    /// User deletion
    /// </summary>
    [HttpDelete("{userId:long}")]
    [Authorize(Policy = AuthenticationConstants.UserIdPolicy)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [SwaggerOperation(OperationId = "DeleteUser", Tags = ["Debug"])]
    public async Task<IResult> Delete(long userId, CancellationToken cancellationToken = new())
    {
        //TODO The isDeleted flag should be added to avoid abuse of referrals 
        await _deleteUserService.DeleteAsync(userId);
        await _transactionService.CommitAsync();
        return Results.NoContent();
    }
}
#endif