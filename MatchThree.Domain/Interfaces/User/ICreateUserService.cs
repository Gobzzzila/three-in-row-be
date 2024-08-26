using MatchThree.Domain.Models;

namespace MatchThree.Domain.Interfaces.User;

public interface ICreateUserService
{
    Task<UserEntity> CreateAsync(UserEntity userEntity, long referrerId);
    UserEntity Create(UserEntity userEntity);
}