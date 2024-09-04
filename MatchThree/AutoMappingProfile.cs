using AutoMapper;
using MatchThree.API.Mapping;
using MatchThree.API.Models;
using MatchThree.API.Models.Leaderboard;
using MatchThree.API.Models.User;
using MatchThree.BL.Configuration;
using MatchThree.Domain.Models;
using MatchThree.Domain.Models.Leaderboard;
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
        CreateMap<UserCreateRequestDto, UserEntity>();
        CreateMap<UserSignInRequestDto, UserEntity>();
        CreateMap<UserDto, UserEntity>().ReverseMap();
        
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
    }
}