using AutoMapper;

namespace MatchThree.API.Mapping;

public sealed class PathWithArgsResolver(LinkGenerator linkGenerator)
    : IMemberValueResolver<object, object, Tuple<string, object?>, string>
{
    private readonly LinkGenerator _linkGenerator = linkGenerator;

    public string Resolve(object source, 
        object destination, 
        Tuple<string, object?> sourceMember, 
        string destMember, 
        ResolutionContext context)
    {
        var executePath = _linkGenerator.GetPathByName(sourceMember.Item1, sourceMember.Item2);
        return executePath!;
    }
}