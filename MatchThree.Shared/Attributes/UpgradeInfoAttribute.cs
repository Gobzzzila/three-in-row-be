namespace MatchThree.Shared.Attributes;

[AttributeUsage(AttributeTargets.Field)]
public class UpgradeInfoAttribute(string headerTextId, 
    string descriptionTextId, 
    string? blockingTextId = default) 
    : Attribute
{
    public string HeaderTextId { get; } = headerTextId;
    public string DescriptionTextId { get; } = descriptionTextId;
    public string? BlockingTextId { get; } = blockingTextId;
}