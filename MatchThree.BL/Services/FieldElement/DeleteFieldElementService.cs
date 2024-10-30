using MatchThree.Domain.Interfaces.FieldElement;
using MatchThree.Repository.MSSQL;
using MatchThree.Repository.MSSQL.Models;
using Microsoft.EntityFrameworkCore;

namespace MatchThree.BL.Services.FieldElement;

public class DeleteFieldElementService(MatchThreeDbContext context) 
    : IDeleteFieldElementService
{
    private readonly MatchThreeDbContext _context = context;

    public async Task DeleteByUserIdAsync(long userId)
    {
        var dbModels = await _context.Set<FieldElementDbModel>()
            .Where(x => x.UserId == userId)
            .ToListAsync();
        
        _context.Set<FieldElementDbModel>().RemoveRange(dbModels);
    }
}