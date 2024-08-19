namespace MatchThree.Domain.Interfaces.User;

public interface IDeleteUserService
{
    Task DeleteAsync(long id);
}