using MatchThree.Domain.Interfaces.Energy;
using MatchThree.Repository.MSSQL;
using MatchThree.Repository.MSSQL.Models;
using MatchThree.Shared.Exceptions;

namespace MatchThree.BL.Services.Energy;

public class DeleteEnergyService(MatchThreeDbContext context) : IDeleteEnergyService
{
    private readonly MatchThreeDbContext _context = context;

    public async Task DeleteAsync(long id)
    {
        var dbModel = await _context.Set<EnergyDbModel>().FindAsync(id);
        if (dbModel is null)
            throw new NoDataFoundException();
        
        _context.Set<EnergyDbModel>().Remove(dbModel);
    }
}