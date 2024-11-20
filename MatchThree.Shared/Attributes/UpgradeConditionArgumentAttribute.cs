namespace MatchThree.Shared.Attributes;

[AttributeUsage(AttributeTargets.Field)]
public class UpgradeConditionArgumentAttribute<T>(T arg) : Attribute
{
    public T Arg { get; } = arg;
}