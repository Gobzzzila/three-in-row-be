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
        UserMapping();
        BalanceMapping();
        LeaderboardMemberMapping();
    }

    private void UserMapping()
    {
        CreateMap<UserCreateDto, UserEntity>();
        CreateMap<UserDto, UserEntity>().ReverseMap();
    }
    
    private void BalanceMapping()
    {
        CreateMap<BalanceEntity, BalanceDto>();
    }
    
    private void LeaderboardMemberMapping()
    {
        CreateMap<LeaderboardMemberEntity, LeaderboardMemberDto>();
    }
}