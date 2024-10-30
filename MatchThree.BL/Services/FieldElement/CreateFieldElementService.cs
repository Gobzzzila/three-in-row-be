using MatchThree.BL.Configuration;
using MatchThree.Domain.Interfaces.FieldElement;
using MatchThree.Repository.MSSQL;
using MatchThree.Repository.MSSQL.Models;

namespace MatchThree.BL.Services.FieldElement;

public class CreateFieldElementService(MatchThreeDbContext context) 
    : ICreateFieldElementService
{
    private readonly MatchThreeDbContext _context = context;

    public void Create(long userId)
    {
        var startValues = FieldElementsConfiguration.GetStartValue();
        _context.Set<FieldElementDbModel>()
            .AddRange(startValues.Select(x => new FieldElementDbModel
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Element = x.cryptoType,
                Level = x.elementLevel
        }));
    }
}