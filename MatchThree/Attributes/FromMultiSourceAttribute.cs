using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MatchThree.API.Attributes;

[AttributeUsage(AttributeTargets.Parameter)]
public class FromMultiSourceAttribute : Attribute, IBindingSourceMetadata
{
    public BindingSource BindingSource { get; } =
        CompositeBindingSource.Create(new[] { BindingSource.Path, BindingSource.Query },
            nameof(FromMultiSourceAttribute));
}