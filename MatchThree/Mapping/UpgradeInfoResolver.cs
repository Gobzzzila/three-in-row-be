using AutoMapper;
using MatchThree.API.Models.Upgrades;
using MatchThree.Domain.Models.Upgrades;
using MatchThree.Shared.Constants;
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

        if (source.IsBlocked)
            destination.BlockingText = ResolveWithArgs(upgradeInfo!.BlockingTextId!, source.BlockingTextArgs);
        
        destination.DescriptionText = source.Price is not null 
            ? ResolveWithArgs(upgradeInfo!.DescriptionTextId, source.DescriptionTextArgs) 
            : ResolveWithArgs(TranslationConstants.CommonMaxLevelReachedTextKey, source.BlockingTextArgs); //TODO mb wrong place for logic

        return _localizer[upgradeInfo!.HeaderTextId];
    }

    private string ResolveWithArgs(string textId, object?[] textArgs)
    {
        return string.Format(_localizer[textId], textArgs);
    }
}