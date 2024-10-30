using AutoMapper;
using MatchThree.Domain.Interfaces.FieldElement;
using MatchThree.Domain.Models;
using MatchThree.Repository.MSSQL;
using MatchThree.Repository.MSSQL.Models;
using MatchThree.Shared.Enums;
using MatchThree.Shared.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace MatchThree.BL.Services.FieldElement;

public class ReadFieldElementService(MatchThreeDbContext context, 
    IMapper mapper)
    : IReadFieldElementService
{
    private readonly MatchThreeDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<List<FieldElementEntity>> GetByUserIdAsync(long userId)
    {
        var dbModels = await _context.Set<FieldElementDbModel>()
            .AsNoTracking()
            .Where(x => x.UserId == userId)
            .ToListAsync();
        
        return _mapper.Map<List<FieldElementEntity>>(dbModels);
    }
}