namespace MatchThree.Shared.Attributes;

[AttributeUsage(AttributeTargets.Field)]
public sealed class TranslationIdAttribute(string textId) : Attribute
{
    public string TextId { get; } = textId;
}