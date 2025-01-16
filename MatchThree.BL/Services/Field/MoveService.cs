using System.Security.Cryptography;
using System.Text;
using MatchThree.Domain.Interfaces.Balance;
using MatchThree.Domain.Interfaces.Energy;
using MatchThree.Domain.Interfaces.Field;
using MatchThree.Repository.MSSQL;
using MatchThree.Repository.MSSQL.Models;
using MatchThree.Shared.Enums;
using MatchThree.Shared.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MatchThree.BL.Services.Field;

public class MoveService(MatchThreeDbContext context,
    ISynchronizationEnergyService synchronizationEnergyService,
    IUpdateFieldService updateFieldService,
    IUpdateEnergyService updateEnergyService,
    IUpdateBalanceService updateBalanceService,
    ILogger<MoveService> logger) 
    : IMoveService
{
    private readonly MatchThreeDbContext _context = context;
    private readonly ISynchronizationEnergyService _synchronizationEnergyService = synchronizationEnergyService;
    private readonly IUpdateFieldService _updateFieldService = updateFieldService;
    private readonly IUpdateEnergyService _updateEnergyService = updateEnergyService;
    private readonly IUpdateBalanceService _updateBalanceService = updateBalanceService;
    private readonly ILogger<MoveService> _logger = logger;                     //DRAFT

    public async Task MakeMoveAsync(long userId, uint reward, int[][] field, string hash)
    {
        var userDbModel = await _context.Set<UserDbModel>()
            .Include(x => x.FieldElementLevel)                                                      //DRAFT
            .ThenInclude(x => x!.FieldElements!.Where(y => y.Level != ElementLevels.Undefined))     //DRAFT
            .Include(x => x.Energy)
            .FirstOrDefaultAsync(x => x.Id == userId);
        
        if (userDbModel?.Energy is null)
            throw new NoDataFoundException();
        
        _synchronizationEnergyService.SynchronizeModel(userDbModel.Energy);
        var calculatedHash = 
            CalculateHash($"{reward}{userId}{userDbModel.Energy.CurrentReserve}{userDbModel.SessionHash}");

        if (!calculatedHash.Equals(hash, StringComparison.OrdinalIgnoreCase))
            throw new ValidationException();
        
        _logger.LogInformation($"{userId},{reward}.{string.Join(',', 
            userDbModel.FieldElementLevel!.FieldElements!
                .Select(x => $"{(int)x.Element}:{(int)x.Level}"))}");           //DRAFT
        
        if (reward > 4_863)                             //DRAFT
            throw new ValidationException();            //DRAFT
        
        await _updateEnergyService.SpendEnergyAsync(userId);
        await _updateFieldService.UpdateFieldAsync(userId, field);
        await _updateBalanceService.AddBalanceAsync(userId, reward);
    }

    private static string CalculateHash(string input)
    {
        var data = MD5.HashData(Encoding.ASCII.GetBytes(input));
        var sBuilder = new StringBuilder();
        foreach (var t in data)
            sBuilder.Append(t.ToString("x2"));

        return sBuilder.ToString();
    }
}