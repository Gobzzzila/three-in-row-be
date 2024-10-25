using AutoMapper;
using MatchThree.Domain.Interfaces.Energy;
using MatchThree.Domain.Models;
using MatchThree.Repository.MSSQL;
using MatchThree.Repository.MSSQL.Models;
using MatchThree.Shared.Exceptions;

namespace MatchThree.BL.Services.Energy;

public class ReadEnergyService(MatchThreeDbContext context, 
    ISynchronizationEnergyService synchronizationEnergyService,
    IMapper mapper) 
    : IReadEnergyService
{
    private readonly MatchThreeDbContext _context = context;
    private readonly ISynchronizationEnergyService _synchronizationEnergyService = synchronizationEnergyService;
    private readonly IMapper _mapper = mapper;

    public async Task<EnergyEntity> GetByUserIdAsync(long userId)
    {
        var dbModel = await _context.Set<EnergyDbModel>().FindAsync(userId);
        if (dbModel is null)
            throw new NoDataFoundException();

        _synchronizationEnergyService.SynchronizeModel(dbModel);
        
        return _mapper.Map<EnergyEntity>(dbModel);
    }
}