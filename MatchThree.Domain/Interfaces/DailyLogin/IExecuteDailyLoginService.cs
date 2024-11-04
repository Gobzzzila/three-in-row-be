namespace MatchThree.Domain.Interfaces.DailyLogin;

public interface IExecuteDailyLoginService
{
    Task ExecuteDailyLogin(long userId);
}