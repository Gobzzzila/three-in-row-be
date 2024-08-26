using AutoMapper;
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
        CreateMap<BalanceDbModel, BalanceEntity>();
        
        //LeaderboardMember
        CreateMap<LeaderboardMemberDbModel, LeaderboardMemberEntity>();
        
        //Energy 
        CreateMap<EnergyDbModel, EnergyEntity>();
    }
}