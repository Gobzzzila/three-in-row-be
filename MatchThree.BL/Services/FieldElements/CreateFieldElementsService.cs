using MatchThree.BL.Configuration;
using MatchThree.Domain.Interfaces.FieldElements;
using MatchThree.Repository.MSSQL;
using MatchThree.Repository.MSSQL.Models;

namespace MatchThree.BL.Services.FieldElements;

public class CreateFieldElementsService(MatchThreeDbContext context) 
    : ICreateFieldElementsService
{
    private readonly MatchThreeDbContext _context = context;

    public void Create(long userId)
    {
        _context.Set<FieldElementsDbModel>().Add(new FieldElementsDbModel
        {
            Id = userId,
            FieldLevel = FieldConfiguration.GetStartValue()
        });
    }
}