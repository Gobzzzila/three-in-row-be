using MatchThree.Domain.Models;

namespace MatchThree.Domain.Interfaces;

public interface ITelegramValidatorService
{
    UserEntity? ValidateTelegramWebAppData(string telegramInitData);
}