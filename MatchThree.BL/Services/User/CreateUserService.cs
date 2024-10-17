using AutoMapper;
using MatchThree.Domain.Interfaces.Balance;
using MatchThree.Domain.Interfaces.Energy;
using MatchThree.Domain.Interfaces.FieldElements;
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
    ICreateFieldElementsService fieldElementsService,
    TimeProvider timeProvider) : ICreateUserService
{
    private readonly MatchThreeDbContext _context = context;
    private readonly IMapper _mapper = mapper;
    private readonly ICreateReferralService _createReferralService = createReferralService;
    private readonly ICreateBalanceService _createBalanceService = createBalanceService;
    private readonly ICreateEnergyService _createEnergyService = createEnergyService;
    private readonly ICreateFieldElementsService _fieldElementsService = fieldElementsService;
    private readonly TimeProvider _dateTimeProvider = timeProvider;

    public UserEntity Create(UserEntity userEntity)
    {
        var result = CreateUser(userEntity);
        CreateSubEntities(result, 0);
        return result;
    }

    public async Task<UserEntity> CreateWithReferrerAsync(UserEntity userEntity, long referrerId)
    {
        var result = CreateUser(userEntity);

        await _createReferralService.CreateAsync(referrerId, result.Id, result.IsPremium);
        var referralReward = userEntity.IsPremium
            ? ReferralConstants.RewardForInvitingPremiumUser
            : ReferralConstants.RewardForInvitingRegularUser;
        
        CreateSubEntities(result, referralReward);
        return result;
    }

    private UserEntity CreateUser(UserEntity userEntity)
    {
        var createDbModel = _mapper.Map<UserDbModel>(userEntity);
        createDbModel.CreatedAt = _dateTimeProvider.GetUtcNow().DateTime;
        createDbModel = _context.Set<UserDbModel>().Add(createDbModel).Entity;
        var result = _mapper.Map<UserEntity>(createDbModel);
        return result;
    }
    
    private void CreateSubEntities(UserEntity userEntity, uint referralReward)
    {
        _createBalanceService.Create(userEntity.Id, referralReward);
        _createEnergyService.Create(userEntity.Id);
        _fieldElementsService.Create(userEntity.Id);
    }
}