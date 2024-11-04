using AutoMapper;
using MatchThree.Domain.Interfaces.Balance;
using MatchThree.Domain.Interfaces.DailyLogin;
using MatchThree.Domain.Interfaces.Energy;
using MatchThree.Domain.Interfaces.Field;
using MatchThree.Domain.Interfaces.FieldElement;
using MatchThree.Domain.Interfaces.Quests;
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
    ICreateFieldService createFieldService,
    ICreateFieldElementService createFieldElementService,
    ICreateCompletedQuestsService createCompletedQuestsService,
    ICreateDailyLoginService createDailyLoginService,
    TimeProvider timeProvider) : ICreateUserService
{
    private readonly MatchThreeDbContext _context = context;
    private readonly IMapper _mapper = mapper;
    private readonly ICreateReferralService _createReferralService = createReferralService;
    private readonly ICreateBalanceService _createBalanceService = createBalanceService;
    private readonly ICreateEnergyService _createEnergyService = createEnergyService;
    private readonly ICreateFieldService _createFieldService = createFieldService;
    private readonly ICreateFieldElementService _createFieldElementService = createFieldElementService;
    private readonly ICreateCompletedQuestsService _createCompletedQuestsService = createCompletedQuestsService;
    private readonly ICreateDailyLoginService _createDailyLoginService = createDailyLoginService;
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
        _createFieldService.Create(userEntity.Id);
        _createFieldElementService.Create(userEntity.Id);
        _createCompletedQuestsService.Create(userEntity.Id);
        _createDailyLoginService.Create(userEntity.Id);
    }
}