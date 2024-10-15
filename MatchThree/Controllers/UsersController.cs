using MatchThree.API.Attributes;
using MatchThree.API.Models.Users;
using MatchThree.Domain.Interfaces;
using MatchThree.Domain.Interfaces.User;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace MatchThree.API.Controllers;

[ApiController]
[Route("api/v1/users")]
public class UsersController(IReadUserService readUserService,
    ICreateUserService createUserService,
    IUpdateUserService updateUserService,
    ITelegramValidatorService telegramValidatorService,
    IJwtTokenService jwtTokenService,
    ITransactionService transactionService)
{
    private readonly IReadUserService _readUserService = readUserService;
    private readonly ICreateUserService _createUserService = createUserService;
    private readonly IUpdateUserService _updateUserService = updateUserService;
    private readonly ITelegramValidatorService _telegramValidatorService = telegramValidatorService;
    private readonly IJwtTokenService _jwtTokenService = jwtTokenService;
    private readonly ITransactionService _transactionService = transactionService;

    /// <summary>
    /// User creation
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
    [SwaggerOperation(OperationId = "SignUp", Tags = ["Users"])]
    public async Task<IResult> SignUp([FromMultiSource] UserRequestDto request, 
        CancellationToken cancellationToken = new())
    {
        var userEntityFromRequest = _telegramValidatorService.ValidateTelegramWebAppData(request.InitData);
        if (userEntityFromRequest is null)
            throw new Exception("Init data doesn't contain user info");
        
        var userEntity = await _readUserService.GetByIdAsync(userEntityFromRequest.Id);
        if (userEntity is null)
        {
            if (request.ReferrerId.HasValue)
                await _createUserService.CreateWithReferrerAsync(userEntityFromRequest, request.ReferrerId.Value);
            else
                _createUserService.Create(userEntityFromRequest);
        }
        else
        {
            await _updateUserService.SyncUserData(userEntityFromRequest);
        }
        
        var token = _jwtTokenService.GenerateJwtToken(userEntityFromRequest.Id);
        await _transactionService.Commit();
        return Results.Ok(token);
    }
}