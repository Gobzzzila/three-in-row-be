using AutoMapper;
using MatchThree.API.Mapping;
using MatchThree.API.Models;
using MatchThree.API.Models.Leaderboard;
using MatchThree.API.Models.Upgrades;
using MatchThree.API.Models.Users;
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
            .ForMember(x => x.League, 
                o => o.MapFrom(s => LeagueConfiguration.CalculateLeague(s.OverallBalance)));

        //LeaderboardMemberMapping
        CreateMap<LeaderboardMemberEntity, LeaderboardMemberDto>();
        CreateMap<LeaderboardEntity, LeaderboardDto>()
            .ForMember(x => x.LeagueName,
            o =>
                o.MapFrom<EnumTranslationResolver<LeagueTypes>, LeagueTypes>(s => s.League));
        
        //Energy
        CreateMap<EnergyEntity, EnergyDto>()
            .ForMember(x => x.MaxReserve, 
                o => o.MapFrom(s => EnergyReserveConfiguration.GetReserveMaxValue(s.MaxReserve)))
            .ForMember(x => x.RecoveryTime,
                o => o.MapFrom(s => EnergyRecoveryConfiguration.GetRecoveryTime(s.RecoveryLevel)))
            .ForMember(x => x.NearbyEnergyRecoveryAt,
                o => o.MapFrom(s => s.LastRecoveryStartTime + EnergyRecoveryConfiguration.GetRecoveryTime(s.RecoveryLevel)));
        
        //Referrals 
        CreateMap<ReferralEntity, ReferralDto>();
        
        //Upgrades 
        CreateMap<UpgradeEntity, UpgradeDto>()
            .ForMember(x => x.HeaderText,
                o =>
                    o.MapFrom<EnumTranslationResolver<UpgradeTypes>, UpgradeTypes>(s => s.Type))
            .ForMember(x => x.DescriptionText,
                o =>
                    o.MapFrom<TextKeyResolver, string>(s => s.DescriptionTextKey))
            .ForMember(x => x.BlockingText,
                o =>
                {
                    o.PreCondition(src => src.BlockingTextKey is not null);
                    o.MapFrom<TextKeyWithArgsResolver, Tuple<string, object?[]>>(s => 
                        Tuple.Create(s.BlockingTextKey!, s.BlockingTextArgs));
                });
        
        CreateMap<GroupedUpgradesEntity, GroupedUpgradesDto>()
            .ForMember(x => x.CategoryName,
                o =>
                    o.MapFrom<EnumTranslationResolver<UpgradeCategories>, UpgradeCategories>(s => s.Category));
    }
}