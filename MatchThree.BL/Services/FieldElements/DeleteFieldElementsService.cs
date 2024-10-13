using MatchThree.Domain.Interfaces.FieldElements;
using MatchThree.Repository.MSSQL;
using MatchThree.Repository.MSSQL.Models;

namespace MatchThree.BL.Services.FieldElements;

public class DeleteFieldElementsService(MatchThreeDbContext context) 
    : IDeleteFieldElementsService
{
    private readonly MatchThreeDbContext _context = context;

    public async Task DeleteAsync(long id)
    {
        var dbModel = await _context.Set<FieldElementsDbModel>().FindAsync(id);
        _context.Set<FieldElementsDbModel>().Remove(dbModel!);
    }
}