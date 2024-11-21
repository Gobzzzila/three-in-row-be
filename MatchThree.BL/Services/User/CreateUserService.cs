using System.Net;
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
using MatchThree.Shared.Exceptions;

namespace MatchThree.BL.Services.User;

public sealed class CreateUserService(MatchThreeDbContext context, 
    IMapper mapper, 
    IReadUserService readUserService,
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
    private readonly IReadUserService _readUserService = readUserService;
    private readonly ICreateReferralService _createReferralService = createReferralService;
    private readonly ICreateBalanceService _createBalanceService = createBalanceService;
    private readonly ICreateEnergyService _createEnergyService = createEnergyService;
    private readonly ICreateFieldService _createFieldService = createFieldService;
    private readonly ICreateFieldElementService _createFieldElementService = createFieldElementService;
    private readonly ICreateCompletedQuestsService _createCompletedQuestsService = createCompletedQuestsService;
    private readonly ICreateDailyLoginService _createDailyLoginService = createDailyLoginService;
    private readonly TimeProvider _dateTimeProvider = timeProvider;

    public void Create(UserEntity userEntity)
    {
        CreateUser(userEntity);
        CreateSubEntities(userEntity.Id, 0);
    }

    public async Task CreateWithReferrerAsync(UserEntity userEntity, long referrerId)
    {
        CreateUser(userEntity);

        var referrer = await _readUserService.FindByIdAsync(referrerId);
        if (referrer is null)
            throw new ValidationException(TranslationConstants.ExceptionReferralLinkTextKey, [], HttpStatusCode.FailedDependency);
        
        await _createReferralService.CreateAsync(referrerId, userEntity.Id, userEntity.IsPremium);
        var referralReward = userEntity.IsPremium
            ? ReferralConstants.RewardForInvitingPremiumUser
            : ReferralConstants.RewardForInvitingRegularUser;
        
        CreateSubEntities(userEntity.Id, referralReward);
    }

    private void CreateUser(UserEntity userEntity)
    {
        var createDbModel = _mapper.Map<UserDbModel>(userEntity);
        createDbModel.CreatedAt = _dateTimeProvider.GetUtcNow().DateTime;
        _context.Set<UserDbModel>().Add(createDbModel);
    }
    
    private void CreateSubEntities(long userId, uint referralReward)
    {
        _createBalanceService.Create(userId, referralReward);
        _createEnergyService.Create(userId);
        _createFieldService.Create(userId);
        _createFieldElementService.Create(userId);
        _createCompletedQuestsService.Create(userId);
        _createDailyLoginService.Create(userId);
    }
}