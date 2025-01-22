using System.Globalization;
using MatchThree.Domain.Interfaces.UserSettings;
using MatchThree.Repository.MSSQL;
using MatchThree.Repository.MSSQL.Models;
using MatchThree.Shared.Extensions;
using CultureTypes = MatchThree.Shared.Enums.CultureTypes;

namespace MatchThree.BL.Services.UserSettings;

public class CreateUserSettingsService (MatchThreeDbContext context)
    : ICreateUserSettingsService
{
    private readonly MatchThreeDbContext _context = context;

    public void Create(long userId)
    {
        _context.Set<UserSettingsDbModel>().Add(new UserSettingsDbModel
        {
            Id = userId,
            Notifications = true,
            Culture = CultureInfo.CurrentCulture.Name.AcceptLanguageToCultureTypes()
        });
    }
}