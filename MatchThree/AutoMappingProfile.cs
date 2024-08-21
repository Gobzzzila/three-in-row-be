using AutoMapper;
using MatchThree.API.Models;
using MatchThree.API.Models.User;
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
        CreateMap<BalanceEntity, BalanceDto>();

        //LeaderboardMemberMapping
        CreateMap<LeaderboardMemberEntity, LeaderboardMemberDto>();
        
        //Energy
        CreateMap<EnergyEntity, EnergyDto>();
    }
}