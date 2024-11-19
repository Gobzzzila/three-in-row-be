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
            Field = new int[][]
            {
                [ 0, 0, 4, 5, 3, 3, 1, 0, 0 ],
                [ 0, 0, 2, 4, 2, 1, 1, 0, 0 ],
                [ 0, 0, 4, 5, 1, 3, 4, 0, 0 ],
                [ 0, 0, 2, 2, 5, 2, 1, 0, 0 ],
                [ 0, 0, 5, 5, 2, 3, 1, 0, 0 ],
                [ 0, 0, 0, 0, 0, 0, 0, 0, 0 ],
                [ 0, 0, 0, 0, 0, 0, 0, 0, 0 ],
                [ 0, 0, 0, 0, 0, 0, 0, 0, 0 ],
                [ 0, 0, 0, 0, 0, 0, 0, 0, 0 ]
            },
            FieldLevel = FieldConfiguration.GetStartValue()
        });
    }
}