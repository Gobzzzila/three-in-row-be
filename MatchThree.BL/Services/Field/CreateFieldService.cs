using MatchThree.BL.Configuration;
using MatchThree.Domain.Interfaces.Field;
using MatchThree.Repository.MSSQL;
using MatchThree.Repository.MSSQL.Models;

namespace MatchThree.BL.Services.Field;

public class CreateFieldService(MatchThreeDbContext context) 
    : ICreateFieldService
{
    private readonly MatchThreeDbContext _context = context;

    public void Create(long userId)
    {
        _context.Set<FieldDbModel>().Add(new FieldDbModel
        {
            Id = userId,
            FieldLevel = FieldConfiguration.GetStartValue()
        });
    }
}