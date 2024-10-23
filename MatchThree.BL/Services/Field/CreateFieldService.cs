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
        var field = new int[9][];
        var random = new Random();
        for (var i = 0; i < 9; i++)
        {
            field[i] = new int[9];
            for (var j = 0; j < 9; j++)
            {
                if (i is >= 0 and <= 4 && j is >= 2 and <= 6)
                    field[i][j] = random.Next(1, 6);
                else
                    field[i][j] = 0;
            }
        }

        _context.Set<FieldDbModel>().Add(new FieldDbModel
        {
            Id = userId,
            Field = field,
            FieldLevel = FieldConfiguration.GetStartValue()
        });
    }
}