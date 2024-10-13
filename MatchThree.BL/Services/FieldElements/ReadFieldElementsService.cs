using AutoMapper;
using MatchThree.Domain.Interfaces.FieldElements;
using MatchThree.Domain.Models;
using MatchThree.Repository.MSSQL;
using MatchThree.Repository.MSSQL.Models;
using MatchThree.Shared.Exceptions;

namespace MatchThree.BL.Services.FieldElements;

public class ReadFieldElementsService(MatchThreeDbContext context, 
    IMapper mapper) 
    : IReadFieldElementsService
{
    private readonly MatchThreeDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<FieldElementsEntity> GetByUserIdAsync(long userId)
    {
        var dbModel = await _context.Set<FieldElementsDbModel>().FindAsync(userId);
        
        if (dbModel is null)
            throw new NoDataFoundException();

        return _mapper.Map<FieldElementsEntity>(dbModel);
    }
}