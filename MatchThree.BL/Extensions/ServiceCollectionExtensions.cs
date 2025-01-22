using MatchThree.BL.Services;
using MatchThree.BL.Services.Balance;
using MatchThree.BL.Services.DailyLogin;
using MatchThree.BL.Services.Energy;
using MatchThree.BL.Services.Field;
using MatchThree.BL.Services.FieldElement;
using MatchThree.BL.Services.LeaderboardMember;
using MatchThree.BL.Services.Quests;
using MatchThree.BL.Services.Referral;
using MatchThree.BL.Services.Upgrades;
using MatchThree.BL.Services.User;
using MatchThree.BL.Services.UserSettings;
using MatchThree.Domain.Interfaces;
using MatchThree.Domain.Interfaces.Balance;
using MatchThree.Domain.Interfaces.DailyLogin;
using MatchThree.Domain.Interfaces.Energy;
using MatchThree.Domain.Interfaces.Field;
using MatchThree.Domain.Interfaces.FieldElement;
using MatchThree.Domain.Interfaces.LeaderboardMember;
using MatchThree.Domain.Interfaces.Quests;
using MatchThree.Domain.Interfaces.Referral;
using MatchThree.Domain.Interfaces.Upgrades;
using MatchThree.Domain.Interfaces.User;
using MatchThree.Domain.Interfaces.UserSettings;
using Microsoft.Extensions.DependencyInjection;

namespace MatchThree.BL.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddDomainServices(this IServiceCollection services)
    {
        services.AddSingleton(TimeProvider.System);
        services.AddScoped<ITransactionService, TransactionService>();
        
        //User
        services.AddScoped<ICreateUserService, CreateUserService>();
        services.AddScoped<IDeleteUserService, DeleteUserService>();
        services.AddScoped<IUpdateUserService, UpdateUserService>();
        services.AddScoped<IReadUserService, ReadUserService>();
        
        //Referral
        services.AddScoped<ICreateReferralService, CreateReferralService>();
        services.AddScoped<IDeleteReferralService, DeleteReferralService>();
        services.AddScoped<IReadReferralService, ReadReferralService>();
        
        //Balance
        services.AddScoped<ICreateBalanceService, CreateBalanceService>();
        services.AddScoped<IDeleteBalanceService, DeleteBalanceService>();
        services.AddScoped<IUpdateBalanceService, UpdateBalanceService>();
        services.AddScoped<IReadBalanceService, ReadBalanceService>();
        
        //LeaderboardMember
        services.AddScoped<IDeleteLeaderboardMemberService, DeleteLeaderboardMemberService>();
        services.AddScoped<ICreateLeaderboardMemberService, CreateLeaderboardMemberService>();
        services.AddScoped<IReadLeaderboardMemberService, ReadLeaderboardMemberService>();
        
        //Energy
        services.AddScoped<ICreateEnergyService, CreateEnergyService>();
        services.AddScoped<IDailyRefillsService, DailyRefillsService>();
        services.AddScoped<IReadEnergyService, ReadEnergyService>();
        services.AddTransient<ISynchronizationEnergyService, SynchronizationEnergyService>();
        services.AddScoped<IUpdateEnergyService, UpdateEnergyService>();
        services.AddScoped<IDeleteEnergyService, DeleteEnergyService>();
        
        //Field
        services.AddScoped<ICreateFieldService, CreateFieldService>();
        services.AddScoped<IDeleteFieldService, DeleteFieldService>();
        services.AddScoped<IUpdateFieldService, UpdateFieldService>();
        services.AddScoped<IReadFieldService, ReadFieldService>();
        services.AddScoped<IMoveService, MoveService>();
        
        //Upgrades
        services.AddScoped<IUpgradesRestrictionsService, UpgradesRestrictionsService>();
        services.AddScoped<IGetUpgradesService, GetUpgradesService>();
        
        //Field elements
        services.AddScoped<ICreateFieldElementService, CreateFieldElementService>();
        services.AddScoped<IDeleteFieldElementService, DeleteFieldElementService>();
        services.AddScoped<IReadFieldElementService, ReadFieldElementService>();
        services.AddScoped<IUpdateFieldElementService, UpdateFieldElementService>();
        
        //Quests
        services.AddScoped<ICreateCompletedQuestsService, CreateCompletedQuestsService>();
        services.AddScoped<IDeleteCompletedQuestsService, DeleteCompletedQuestsService>();
        services.AddScoped<IValidateQuestCompletionService, ValidateQuestCompletionService>();
        services.AddScoped<IReadQuestService, ReadQuestService>();
        services.AddScoped<ICompleteQuestService, CompleteQuestService>();
        
        //Daily login
        services.AddScoped<ICreateDailyLoginService, CreateDailyLoginService>();
        services.AddScoped<IDeleteDailyLoginService, DeleteDailyLoginService>();
        services.AddScoped<IReadDailyLoginService, ReadDailyLoginService>();
        services.AddScoped<IExecuteDailyLoginService, ExecuteDailyLoginService>();
        
        //User settings 
        services.AddScoped<ICreateUserSettingsService, CreateUserSettingsService>();
        services.AddScoped<IReadUserSettingsService, ReadUserSettingsService>();
    }
}