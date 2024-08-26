using AutoMapper;
using MatchThree.Domain.Configuration;
using MatchThree.Domain.Models;
using MatchThree.Repository.MSSQL.Models;

namespace MatchThree.BL;

public class AutoMappingProfile : Profile
{
    public AutoMappingProfile()
    {
        DbModelToEntityConfigurations();
    }

    private void DbModelToEntityConfigurations()
    {
        //User
        CreateMap<UserDbModel, UserEntity>().ReverseMap();
        
        //Balance
        CreateMap<BalanceDbModel, BalanceEntity>()
            .ForMember(x => x.League, 
                o => o.MapFrom(s => LeagueConfiguration.CalculateLeague(s.OverallBalance)));
        
        //LeaderboardMember
        CreateMap<LeaderboardMemberDbModel, LeaderboardMemberEntity>();
        
        //Energy 
        CreateMap<EnergyDbModel, EnergyEntity>()
            .ForMember(x => x.MaxReserve, 
                o => o.MapFrom(s => EnergyReserveConfiguration.GetReserveMaxValue(s.MaxReserve)))
            .ForMember(x => x.RecoveryTime,
                o => o.MapFrom(s => EnergyRecoveryConfiguration.GetRecoveryTime(s.RecoveryLevel)))
            .ForMember(x => x.NearbyEnergyRecoveryAt,
                o => o.MapFrom(s => s.LastRecoveryStartTime + EnergyRecoveryConfiguration.GetRecoveryTime(s.RecoveryLevel)));
    }
}