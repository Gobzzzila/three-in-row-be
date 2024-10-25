using MatchThree.Domain.Models;

namespace MatchThree.Domain.Interfaces.User;

public interface IReadUserService
{
    Task<UserEntity?> FindByIdAsync(long userId);
}