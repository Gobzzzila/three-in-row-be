using AutoMapper;
using MatchThree.Domain.Interfaces.User;
using MatchThree.Domain.Models;
using MatchThree.Repository.MSSQL;
using MatchThree.Repository.MSSQL.Models;
using Microsoft.EntityFrameworkCore;

namespace MatchThree.BL.Services.User;

public class ReadUserService(MatchThreeDbContext context, 
    IMapper mapper) 
    : IReadUserService
{
    private readonly MatchThreeDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<UserEntity?> FindByIdAsync(long userId)
    {
        var dbModel = await _context.Set<UserDbModel>()
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == userId);
        return _mapper.Map<UserEntity?>(dbModel);
    }
}