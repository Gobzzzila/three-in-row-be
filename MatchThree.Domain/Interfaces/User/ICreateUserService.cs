using MatchThree.Domain.Models;

namespace MatchThree.Domain.Interfaces.User;

public interface ICreateUserService
{
    Task CreateWithReferrerAsync(UserEntity userEntity, long referrerId);
    
    void Create(UserEntity userEntity);
}