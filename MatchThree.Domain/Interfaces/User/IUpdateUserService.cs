using MatchThree.Domain.Models;

namespace MatchThree.Domain.Interfaces.User;

public interface IUpdateUserService
{
    Task SyncUserData(UserEntity entity);
}