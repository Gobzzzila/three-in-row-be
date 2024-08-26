using MatchThree.Domain.Interfaces.Balance;
using MatchThree.Repository.MSSQL;
using MatchThree.Repository.MSSQL.Models;
using MatchThree.Shared.Constants;

namespace MatchThree.BL.Services.Balance;

public class CreateBalanceService (MatchThreeDbContext context)
    : ICreateBalanceService
{
    public void Create(long userId, uint initialBalance)
    {
        initialBalance += BalanceConstants.InitialBalanceValue;
        context.Set<BalanceDbModel>().Add(new BalanceDbModel
        {
            Id = userId,
            Balance = initialBalance,
            OverallBalance = initialBalance
        });
    }
}