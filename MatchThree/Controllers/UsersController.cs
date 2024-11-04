using System.Text.Json;
using System.Web;
using MatchThree.API.Attributes;
using MatchThree.API.Models.Users;
using MatchThree.Domain.Interfaces;
using MatchThree.Domain.Interfaces.User;
using MatchThree.Domain.Models;
using MatchThree.Domain.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Annotations;
using Telegram.Bot;

namespace MatchThree.API.Controllers;

[ApiController]
[Route("api/v1/users")]
public class UsersController(IReadUserService readUserService,
    ICreateUserService createUserService,
    IUpdateUserService updateUserService,
    IJwtTokenService jwtTokenService,
    IOptions<TelegramSettings> settings,
    ITransactionService transactionService)
{
    private readonly IReadUserService _readUserService = readUserService;
    private readonly ICreateUserService _createUserService = createUserService;
    private readonly IUpdateUserService _updateUserService = updateUserService;
    private readonly IJwtTokenService _jwtTokenService = jwtTokenService;
    private readonly TelegramSettings _settings = settings.Value;
    private readonly ITransactionService _transactionService = transactionService;

    /// <summary>
    /// User creation
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
    [SwaggerOperation(OperationId = "SignUp", Tags = ["Users"])]
    public async Task<IResult> SignUp([FromMultiSource] UserRequestDto request,         //TODO There's a lot of logic here, mb implement service
        CancellationToken cancellationToken = new())
    {
        var parsedInitData = AuthHelpers.ParseValidateData(request.InitData, _settings.BotToken);
        var userString = parsedInitData.GetValueOrDefault("user");
        if (string.IsNullOrEmpty(userString))
            throw new Exception("User info is not set");        //TODO implement special exception
        
        var userEntityFromRequest = JsonSerializer.Deserialize<UserEntity>(userString);
        if (userEntityFromRequest is null)
            throw new Exception("Init data doesn't contain user info");
        
        var data = HttpUtility.ParseQueryString(request.InitData);
        userEntityFromRequest.SessionHash = data["hash"]!;
        
        var userEntity = await _readUserService.FindByIdAsync(userEntityFromRequest.Id);
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
        await _transactionService.CommitAsync();
        return Results.Ok(token);
    }
}