using AutoMapper;
using Microsoft.Extensions.Localization;

namespace MatchThree.API.Mapping;

public sealed class TextKeyWithArgsResolver(IStringLocalizer<Localization> localizer)
    : IMemberValueResolver<object, object, Tuple<string, object?[]>, string>
{
    private readonly IStringLocalizer<Localization> _localizer = localizer;

    public string Resolve(object source, 
        object destination, 
        Tuple<string, object?[]> sourceMember, 
        string destMember, 
        ResolutionContext context)
    {
        var localizedResource = _localizer[sourceMember.Item1];
        return string.Format(localizedResource, sourceMember.Item2);
    }
}