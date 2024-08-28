using MatchThree.Domain.Interfaces.Balance;
using MatchThree.Repository.MSSQL;
using MatchThree.Repository.MSSQL.Models;
using MatchThree.Shared.Constants;

namespace MatchThree.BL.Services.Balance;

public class CreateBalanceService (MatchThreeDbContext context)
    : ICreateBalanceService
{
    private readonly MatchThreeDbContext _context = context;

    public void Create(long userId, uint initialBalance)
    {
        initialBalance += BalanceConstants.InitialBalanceValue;
        _context.Set<BalanceDbModel>().Add(new BalanceDbModel
        {
            Id = userId,
            Balance = initialBalance,
            OverallBalance = initialBalance
        });
    }
}