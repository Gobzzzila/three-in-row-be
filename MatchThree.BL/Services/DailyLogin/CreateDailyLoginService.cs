using MatchThree.Domain.Interfaces.DailyLogin;
using MatchThree.Repository.MSSQL;
using MatchThree.Repository.MSSQL.Models;

namespace MatchThree.BL.Services.DailyLogin;

public class CreateDailyLoginService(MatchThreeDbContext context)
    : ICreateDailyLoginService
{
    private readonly MatchThreeDbContext _context = context;
    
    public void Create(long userId)
    {
        _context.Set<DailyLoginDbModel>().Add(new DailyLoginDbModel
        {
            Id = userId,
            LastExecuteDate = DateTime.MinValue,
            StreakCount = 0
        });
    }
}