using AutoMapper;
using MatchThree.API.Models.Users;
using MatchThree.Domain.Interfaces;
using MatchThree.Domain.Interfaces.Energy;
using MatchThree.Domain.Interfaces.User;
using MatchThree.Domain.Models;
using MatchThree.Shared.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace MatchThree.API.Controllers;

[ApiController]
[Route("api/v1/debug")]
public class DebugController(IMapper mapper,
    IReadUserService readUserService,
    ICreateUserService createUserService,
    IUpdateUserService updateUserService,
    IDeleteUserService deleteUserService,
    IJwtTokenService jwtTokenService,
    ITransactionService transactionService,
    IUpdateEnergyService updateEnergyService)
{
    private readonly IMapper _mapper = mapper;
    private readonly IReadUserService _readUserService = readUserService;
    private readonly ICreateUserService _createUserService = createUserService;
    private readonly IUpdateUserService _updateUserService = updateUserService;
    private readonly IDeleteUserService _deleteUserService = deleteUserService;
    private readonly IJwtTokenService _jwtTokenService = jwtTokenService;
    private readonly ITransactionService _transactionService = transactionService;
    private readonly IUpdateEnergyService _updateEnergyService = updateEnergyService;
    
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

        UserEntity entity;
        if (userEntity is null)
        {
            var creationEntity = _mapper.Map<UserEntity>(request);
            entity = request.ReferrerId.HasValue 
                ? await _createUserService.CreateWithReferrerAsync(creationEntity, request.ReferrerId.Value)
                : _createUserService.Create(creationEntity);
        }
        else
        {
            entity = _mapper.Map<UserEntity>(request);
            await _updateUserService.SyncUserData(entity);
        }
        
        var token = _jwtTokenService.GenerateJwtToken(entity.Id);
        await _transactionService.CommitAsync();
        return Results.Ok(token);
    }
    
    /// <summary>
    /// User creation
    /// </summary>
    [HttpPost("{userId:long}/spend-energy")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [SwaggerOperation(OperationId = "SpendEnergy", Tags = ["Debug"])]
    public async Task<IResult> SpendEnergy([FromRoute] long userId,
        CancellationToken cancellationToken = new())
    {
        await _updateEnergyService.SpendEnergy(userId);
        await _transactionService.CommitAsync();
        return Results.Ok();
    }
    
#if DEBUG
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
#endif
}
