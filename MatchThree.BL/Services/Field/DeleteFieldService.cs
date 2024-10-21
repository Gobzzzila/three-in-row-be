using MatchThree.Domain.Interfaces.Field;
using MatchThree.Repository.MSSQL;
using MatchThree.Repository.MSSQL.Models;

namespace MatchThree.BL.Services.Field;

public class DeleteFieldService(MatchThreeDbContext context) 
    : IDeleteFieldService
{
    private readonly MatchThreeDbContext _context = context;

    public async Task DeleteAsync(long id)
    {
        var dbModel = await _context.Set<FieldDbModel>().FindAsync(id);
        _context.Set<FieldDbModel>().Remove(dbModel!);
    }
}