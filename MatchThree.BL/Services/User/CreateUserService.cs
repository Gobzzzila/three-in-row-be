using AutoMapper;
using MatchThree.Domain.Interfaces;
using MatchThree.Domain.Interfaces.Balance;
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
    IDateTimeProvider dateTimeProvider) 
    : ICreateUserService
{
    public async Task<UserEntity> CreateAsync(UserEntity userEntity, long? referralId)
    {
        var createDbModel = mapper.Map<UserDbModel>(userEntity);
        createDbModel.CreatedAt = dateTimeProvider.GetUtcDateTime();
        createDbModel = (await context.Set<UserDbModel>().AddAsync(createDbModel)).Entity;
        var result = mapper.Map<UserEntity>(createDbModel);

        uint initBalance = 0; 
        if (referralId.HasValue)
        {
            await createReferralService.CreateAsync(referralId.Value, result.Id, result.IsPremium);

            initBalance += userEntity.IsPremium
                ? ReferralConstants.RewardPremiumUserForBeingInvited
                : ReferralConstants.RewardRegularUserForBeingInvited;
        }
        
        await createBalanceService.CreateAsync(result.Id, initBalance);
        
        return result;
    }
}