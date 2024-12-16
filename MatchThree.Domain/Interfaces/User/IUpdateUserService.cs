using MatchThree.Domain.Models;

namespace MatchThree.Domain.Interfaces.User;

public interface IUpdateUserService
{
    Task SyncUserDataAsync(UserEntity entity);
    Task RecordAdViewAsync(long userId);
}