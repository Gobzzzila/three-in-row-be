using AutoMapper;
using MatchThree.Domain.Interfaces.Energy;
using MatchThree.Domain.Models;
using MatchThree.Repository.MSSQL;
using MatchThree.Repository.MSSQL.Models;
using MatchThree.Shared.Exceptions;

namespace MatchThree.BL.Services.Energy;

public class ReadEnergyService(MatchThreeDbContext context, 
    ISynchronizationEnergyService synchronizationEnergyService,
    IMapper mapper) : IReadEnergyService
{
    public async Task<EnergyEntity> GetByUserIdAsync(long userId)
    {
        var dbModel = await context.Set<EnergyDbModel>().FindAsync(userId);
        
        if (dbModel is null)
            throw new NoDataFoundException();

        synchronizationEnergyService.SynchronizeModel(dbModel);
        
        return mapper.Map<EnergyEntity>(dbModel);
    }
}