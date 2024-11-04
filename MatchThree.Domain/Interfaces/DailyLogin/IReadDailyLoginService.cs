using MatchThree.Domain.Models;

namespace MatchThree.Domain.Interfaces.DailyLogin;

public interface IReadDailyLoginService
{
    Task<DailyLoginEntity> GetDailyLoginInfoByUserIdAsync(long userId);
}