using AutoMapper;
using Microsoft.Extensions.Localization;

namespace MatchThree.API.Mapping;

public class TextKeyResolver(IStringLocalizer<Localization> localizer)
    : IMemberValueResolver<object, object, string, string>
{
    private readonly IStringLocalizer<Localization> _localizer = localizer;

    public string Resolve(object source, 
        object destination, 
        string sourceMember, 
        string destMember, 
        ResolutionContext context)
    {
        return _localizer[sourceMember];
    }
}