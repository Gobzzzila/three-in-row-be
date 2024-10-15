using AutoMapper;
using MatchThree.Domain.Interfaces.User;
using MatchThree.Domain.Models;
using MatchThree.Repository.MSSQL;
using MatchThree.Repository.MSSQL.Models;

namespace MatchThree.BL.Services.User;

public class ReadUserService(MatchThreeDbContext context, 
    IMapper mapper) 
    : IReadUserService
{
    private readonly MatchThreeDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<UserEntity?> GetByIdAsync(long userId)
    {
        var dbModel = await _context.Set<UserDbModel>().FindAsync(userId);
        return _mapper.Map<UserEntity?>(dbModel);
    }
}