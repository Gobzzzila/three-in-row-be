using AutoMapper;
using MatchThree.API.Models;
using MatchThree.API.Models.User;
using MatchThree.Domain.Configuration;
using MatchThree.Domain.Models;

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
        CreateMap<UserCreateDto, UserEntity>();
        CreateMap<UserDto, UserEntity>().ReverseMap();
        
        //BalanceMapping
        CreateMap<BalanceEntity, BalanceDto>()
            .ForMember(x => x.League, 
                o => o.MapFrom(s => LeagueConfiguration.CalculateLeague(s.OverallBalance)));

        //LeaderboardMemberMapping
        CreateMap<LeaderboardMemberEntity, LeaderboardMemberDto>();
        
        //Energy
        CreateMap<EnergyEntity, EnergyDto>()
            .ForMember(x => x.MaxReserve, 
                o => o.MapFrom(s => EnergyReserveConfiguration.GetReserveMaxValue(s.MaxReserve)))
            .ForMember(x => x.RecoveryTime,
                o => o.MapFrom(s => EnergyRecoveryConfiguration.GetRecoveryTime(s.RecoveryLevel)))
            .ForMember(x => x.NearbyEnergyRecoveryAt,
                o => o.MapFrom(s => s.LastRecoveryStartTime + EnergyRecoveryConfiguration.GetRecoveryTime(s.RecoveryLevel)));
    }
}