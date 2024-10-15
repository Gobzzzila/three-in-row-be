using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Web;
using MatchThree.Domain.Interfaces;
using MatchThree.Domain.Models;

namespace MatchThree.API.Authentication;

public class TelegramValidatorService(TimeProvider timeProvider) : ITelegramValidatorService
{
    private readonly TimeProvider _timeProvider = timeProvider;
    private const string BotToken = "1035113874:AAGrwugWGWepmIgpuX63mXzQHkoXJ_qxEEo";
    private const string ConstantKey = "WebAppData";

    public UserEntity? ValidateTelegramWebAppData(string telegramInitData)
    {
        if (string.IsNullOrEmpty(BotToken))
            throw new Exception("BotToken is not set");
        
        var initData = HttpUtility.ParseQueryString(telegramInitData);
        var hash = initData["hash"];

        if (string.IsNullOrEmpty(hash))
            throw new Exception("Hash is not set");
        
        var dataDict = new SortedDictionary<string, string>(
            initData.AllKeys.ToDictionary(x => x!, x => initData[x]!),
            StringComparer.Ordinal);

        var authDate = initData["auth_date"];
        if (string.IsNullOrEmpty(authDate))
            throw new Exception("auth_date is not set");

        var authTimestamp = int.Parse(authDate);
        var currentTimestamp = _timeProvider.GetUtcNow().ToUnixTimeSeconds();
        var timeDifference = currentTimestamp - authTimestamp;
        
        if (timeDifference > 300)
            throw new Exception("Telegram data is older than 5 minutes");
        
        var dataCheckString = string.Join('\n', 
            dataDict.Where(x => x.Key != "hash")
                .Select(x => $"{x.Key}={x.Value}"));

        var secretKey = HMACSHA256.HashData(Encoding.UTF8.GetBytes(ConstantKey), Encoding.UTF8.GetBytes(BotToken));
        var generatedHash = HMACSHA256.HashData(secretKey, Encoding.UTF8.GetBytes(dataCheckString));
        var actualHash = Convert.FromHexString(dataDict["hash"]);

        if (!actualHash.SequenceEqual(generatedHash)) 
            throw new Exception("Wrong hash");
        
        var userString = dataDict.GetValueOrDefault("user");
        if (string.IsNullOrEmpty(userString))
            throw new Exception("User info is not set");
            
        return JsonSerializer.Deserialize<UserEntity>(userString!);
    }
}