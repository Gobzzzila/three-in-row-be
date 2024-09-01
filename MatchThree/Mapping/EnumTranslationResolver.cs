using AutoMapper;
using MatchThree.Shared.Extensions;
using Microsoft.Extensions.Localization;

namespace MatchThree.API.Mapping;

public sealed class EnumTranslationResolver<TSourceEnum>(IStringLocalizer<Localization> localizer)
    : IMemberValueResolver<object, object, TSourceEnum, string>
    where TSourceEnum : Enum
{
    private readonly IStringLocalizer<Localization> _localizer = localizer;

    public string Resolve(object source, object destination, TSourceEnum sourceMember, string destMember,
        ResolutionContext context)
    {
        var translationId = sourceMember.GetTranslationId();
        return translationId != default ? _localizer[translationId] : string.Empty;
    }
}