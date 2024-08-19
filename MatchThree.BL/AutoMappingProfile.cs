using AutoMapper;
using MatchThree.Domain.Models;
using MatchThree.Domain.Models.Configuration;
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
    }
}