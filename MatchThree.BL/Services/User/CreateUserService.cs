using AutoMapper;
using MatchThree.Domain.Interfaces;
using MatchThree.Domain.Interfaces.Balance;
using MatchThree.Domain.Interfaces.Energy;
using MatchThree.Domain.Interfaces.Referral;
using MatchThree.Domain.Interfaces.User;
using MatchThree.Domain.Models;
using MatchThree.Repository.MSSQL;
using MatchThree.Repository.MSSQL.Models;
using MatchThree.Shared.Constants;

namespace MatchThree.BL.Services.User;

public sealed class CreateUserService(MatchThreeDbContext context, 
    IMapper mapper, 
    ICreateReferralService createReferralService, 
    ICreateBalanceService createBalanceService,
    ICreateEnergyService createEnergyService,
    IDateTimeProvider dateTimeProvider) : ICreateUserService
{
    public UserEntity Create(UserEntity userEntity)
    {
        var result = CreateUser(userEntity);
        CreateSubEntities(result, 0);
        return result;
    }

    public async Task<UserEntity> CreateAsync(UserEntity userEntity, long referrerId)
    {
        var result = CreateUser(userEntity);

        await createReferralService.CreateAsync(referrerId, result.Id, result.IsPremium);
        var referralReward = userEntity.IsPremium
            ? ReferralConstants.RewardPremiumUserForBeingInvited
            : ReferralConstants.RewardRegularUserForBeingInvited;
        
        CreateSubEntities(result, referralReward);
        return result;
    }

    private UserEntity CreateUser(UserEntity userEntity)
    {
        var createDbModel = mapper.Map<UserDbModel>(userEntity);
        createDbModel.CreatedAt = dateTimeProvider.GetUtcDateTime();
        createDbModel = (context.Set<UserDbModel>().Add(createDbModel)).Entity;
        var result = mapper.Map<UserEntity>(createDbModel);
        return result;
    }
    
    private void CreateSubEntities(UserEntity userEntity, uint referralReward)
    {
        createBalanceService.Create(userEntity.Id, referralReward);
        createEnergyService.Create(userEntity.Id);
    }
}