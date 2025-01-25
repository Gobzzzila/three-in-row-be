using AutoMapper;
using MatchThree.Domain.Models;
using MatchThree.Domain.Models.Leaderboard;
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
        
        //Field
        CreateMap<FieldDbModel, FieldEntity>();
        
        //FieldElement
        CreateMap<FieldElementDbModel, FieldElementEntity>();
        
        //UserSettings 
        CreateMap<UserSettingsDbModel, UserSettingsEntity>();
        
        //Notifications 
        CreateMap<NotificationsDbModel, NotificationsEntity>()
            .ForMember(x => x.Culture, o => 
                o.MapFrom(s => s.User!.Settings!.Culture));;
    }
}