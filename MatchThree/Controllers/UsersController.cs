using System.Globalization;
using System.Text.Json;
using System.Web;
using MatchThree.API.Attributes;
using MatchThree.API.Authentication.Interfaces;
using MatchThree.API.Models.Users;
using MatchThree.Domain.Interfaces;
using MatchThree.Domain.Interfaces.User;
using MatchThree.Domain.Interfaces.UserSettings;
using MatchThree.Domain.Models;
using MatchThree.Domain.Settings;
using MatchThree.Shared.Constants;
using MatchThree.Shared.Exceptions;
using MatchThree.Shared.Extensions;
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
    IReadUserSettingsService readUserSettingsService,
    IJwtTokenService jwtTokenService,
    IOptions<TelegramSettings> settings,
    ITransactionService transactionService)
{
    private readonly IReadUserService _readUserService = readUserService;
    private readonly ICreateUserService _createUserService = createUserService;
    private readonly IUpdateUserService _updateUserService = updateUserService;
    private readonly IReadUserSettingsService _readUserSettingsService = readUserSettingsService;
    private readonly IJwtTokenService _jwtTokenService = jwtTokenService;
    private readonly TelegramSettings _settings = settings.Value;
    private readonly ITransactionService _transactionService = transactionService;

    /// <summary>
    /// User creation
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status424FailedDependency, Type = typeof(ProblemDetails))]
    [SwaggerOperation(OperationId = "SignUp", Tags = ["Users"])]
    public async Task<IResult> SignUp([FromMultiSource] UserRequestDto request,         //TODO There's a lot of logic here, need to implement service
        CancellationToken cancellationToken = new())
    {
        var parsedInitData = AuthHelpers.ParseValidateData(request.InitData, _settings.BotToken);
        var userString = parsedInitData.GetValueOrDefault("user");
        if (string.IsNullOrEmpty(userString))
            throw new ValidationException(TranslationConstants.ExceptionAuthorizationTextKey, ["0x121212"]);
        
        var userEntityFromRequest = JsonSerializer.Deserialize<UserEntity>(userString);
        if (userEntityFromRequest is null)
            throw new ValidationException(TranslationConstants.ExceptionAuthorizationTextKey, ["0x131313"]);
        
        var hash = HttpUtility.ParseQueryString(request.InitData)["hash"];
        if (hash is null)
            throw new ValidationException(TranslationConstants.ExceptionAuthorizationTextKey, [$"0x{userEntityFromRequest.Id:X}"]);
        
        userEntityFromRequest.SessionHash = hash;
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
            var userSettings = await _readUserSettingsService.GetByUserIdAsync(userEntityFromRequest.Id);
            var userAcceptLanguage = userSettings.Culture.ToAcceptLanguage();
            CultureInfo.CurrentCulture = new CultureInfo(userAcceptLanguage);
            CultureInfo.CurrentUICulture = new CultureInfo(userAcceptLanguage);
            
            await _updateUserService.SyncUserDataAsync(userEntityFromRequest);
        }
        
        var token = _jwtTokenService.GenerateJwtToken(userEntityFromRequest.Id);
        await _transactionService.CommitAsync();
        return Results.Ok(token);
    }
}