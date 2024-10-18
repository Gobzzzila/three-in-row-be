using AutoMapper;
using MatchThree.API.Models.Upgrades;
using MatchThree.Domain.Models.Upgrades;
using MatchThree.Shared.Extensions;
using Microsoft.Extensions.Localization;

namespace MatchThree.API.Mapping;

public sealed class UpgradeInfoResolver(IStringLocalizer<Localization> localizer) 
    : IValueResolver<UpgradeEntity, UpgradeDto, string>
{
    private readonly IStringLocalizer<Localization> _localizer = localizer;

    public string Resolve(UpgradeEntity source, UpgradeDto destination, string destMember, ResolutionContext context)
    {
        var upgradeInfo = source.Type.GetUpgradeInfo();
        if (source.BlockingTextArgs.Length != 0)
        {
            var localizedResource = _localizer[upgradeInfo!.BlockingTextId!];
            destination.BlockingText = string.Format(localizedResource, source.BlockingTextArgs);
        }
        destination.DescriptionText = _localizer[upgradeInfo!.DescriptionTextId];
        return _localizer[upgradeInfo.HeaderTextId];
    }
}