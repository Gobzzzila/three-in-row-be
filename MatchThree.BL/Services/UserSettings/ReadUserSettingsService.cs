using AutoMapper;
using MatchThree.Domain.Interfaces.UserSettings;
using MatchThree.Domain.Models;
using MatchThree.Repository.MSSQL;
using MatchThree.Repository.MSSQL.Models;
using MatchThree.Shared.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace MatchThree.BL.Services.UserSettings;

public class ReadUserSettingsService(MatchThreeDbContext context, 
    IMapper mapper) 
    : IReadUserSettingsService
{
    private readonly MatchThreeDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<UserSettingsEntity> GetByUserIdAsync(long userId)
    {
        var dbModel = await _context.Set<UserSettingsDbModel>()
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == userId);
        
        if (dbModel is null)
            throw new NoDataFoundException();

        return _mapper.Map<UserSettingsEntity>(dbModel);
    }
}