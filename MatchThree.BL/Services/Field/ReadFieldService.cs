using AutoMapper;
using MatchThree.Domain.Interfaces.Field;
using MatchThree.Domain.Models;
using MatchThree.Repository.MSSQL;
using MatchThree.Repository.MSSQL.Models;
using MatchThree.Shared.Exceptions;

namespace MatchThree.BL.Services.Field;

public class ReadFieldService(MatchThreeDbContext context, 
    IMapper mapper) 
    : IReadFieldService
{
    private readonly MatchThreeDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<FieldEntity> GetByUserIdAsync(long userId)
    {
        var dbModel = await _context.Set<FieldDbModel>().FindAsync(userId);
        
        if (dbModel is null)
            throw new NoDataFoundException();

        return _mapper.Map<FieldEntity>(dbModel);
    }
}