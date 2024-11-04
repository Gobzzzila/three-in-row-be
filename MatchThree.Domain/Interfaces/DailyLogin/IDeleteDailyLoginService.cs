namespace MatchThree.Domain.Interfaces.DailyLogin;

public interface IDeleteDailyLoginService
{
    Task DeleteAsync(long id);
}