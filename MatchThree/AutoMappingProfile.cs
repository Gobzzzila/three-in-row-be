using AutoMapper;
using MatchThree.API.Mapping;
using MatchThree.API.Models;
using MatchThree.API.Models.Leaderboard;
using MatchThree.API.Models.Referrals;
using MatchThree.API.Models.Upgrades;
using MatchThree.API.Models.Users;
using MatchThree.API.Models.UserSettings;
using MatchThree.BL.Configuration;
using MatchThree.Domain.Models;
using MatchThree.Domain.Models.Leaderboard;
using MatchThree.Domain.Models.Upgrades;
using MatchThree.Shared.Enums;

namespace MatchThree.API;

public class AutoMappingProfile : Profile
{
    public AutoMappingProfile()
    {
        DtoToEntityConfigurations();
    }

    private void DtoToEntityConfigurations()
    {
        //UserMapping
        CreateMap<DebugUserRequestDto, UserEntity>();
        
        //BalanceMapping
        CreateMap<BalanceEntity, BalanceDto>()
            .ForMember(x => x.LeagueName,
                o =>
                    o.MapFrom<EnumTranslationResolver<LeagueTypes>, LeagueTypes>(s => s.League));

        //LeaderboardMemberMapping
        CreateMap<LeaderboardMemberEntity, LeaderboardMemberDto>();
        CreateMap<LeaderboardEntity, LeaderboardDto>()
            .ForMember(x => x.LeagueFullName,
                o =>
                    o.MapFrom<TextKeyResolver, string>(s => s.LeagueFullNameTextKey));
        
        //Energy
        CreateMap<EnergyEntity, EnergyDto>()
            .ForMember(x => x.MaxReserve, 
                o => o.MapFrom(s => EnergyReserveConfiguration.GetReserveMaxValue(s.MaxReserve)))
            .ForMember(x => x.RecoveryTimeInSeconds,
                o => o.MapFrom(s => EnergyRecoveryConfiguration.GetRecoveryTime(s.RecoveryLevel).TotalSeconds))
            .ForMember(x => x.NearbyEnergyRecoveryAt, 
                o => o.MapFrom(s => 
                s.LastRecoveryStartTime.HasValue
                    ? new DateTimeOffset(s.LastRecoveryStartTime.Value + EnergyRecoveryConfiguration.GetRecoveryTime(s.RecoveryLevel), TimeSpan.Zero)
                    : (DateTimeOffset?)null));

        //Referrals 
        CreateMap<ReferralEntity, ReferralDto>();
        
        //Upgrades 
        CreateMap<UpgradeEntity, UpgradeDto>()
            .ForMember(x => x.HeaderText,
                o => o.MapFrom<UpgradeInfoResolver>())
            .ForMember(x => x.ExecutePath,
                o => o.MapFrom<PathWithArgsResolver, Tuple<string, object?>>(s => 
                        Tuple.Create(s.ExecutePathName!, s.ExecutePathArgs)));
        
        CreateMap<GroupedUpgradesEntity, GroupedUpgradesDto>()
            .ForMember(x => x.CategoryName,
                o =>
                    o.MapFrom<EnumTranslationResolver<UpgradeCategories>, UpgradeCategories>(s => s.Category));

        //Quests
        CreateMap<QuestEntity, QuestDto>()
            .ForMember(x => x.Tittle,
                o =>
                    o.MapFrom<TextKeyResolver, string>(s => s.TittleKey))
            .ForMember(x => x.Description,
                o =>
                    o.MapFrom<TextKeyResolver, string>(s => s.DescriptionKey))
            .ForMember(x => x.ActionLink,
                o =>
                {
                    o.PreCondition(s => s.ActionLinkKey is not null);
                    o.MapFrom<TextKeyResolver, string>(s => s.ActionLinkKey!);
                })
            .ForMember(x => x.IsWithSecretCode, 
                o => o.MapFrom(s => !string.IsNullOrEmpty(s.SecretCode)));
        
        //Daily login
        CreateMap<DailyLoginEntity, DailyLoginDto>();
        
        //User settings 
        CreateMap<UserSettingsEntity, UserSettingsDto>()
            .ForMember(x => x.Culture, o => 
                    o.MapFrom(s => s.Culture.ToString()));
    }
}