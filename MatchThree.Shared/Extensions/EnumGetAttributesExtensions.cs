using System.Collections.Concurrent;
using System.Reflection;
using MatchThree.Shared.Attributes;
using MatchThree.Shared.Enums;

namespace MatchThree.Shared.Extensions;

public static class EnumGetAttributesExtensions
{
    #region Without cache

    public static uint? GetUpgradeCost<T>(this T enumValue) where T : Enum
    {
        var fieldName = Enum.GetName(typeof(T), enumValue);
        if (fieldName == null)
            throw new InvalidOperationException($"Value {enumValue} is not defined for enum type {typeof(T).Name}");

        return typeof(T).GetField(fieldName)?.GetCustomAttribute<UpgradeCostAttribute>()?.UpgradeCost;
    }
    
    public static T1 GetUpgradeConditionArgument<T1, T2>(this T2 enumValue) where T2 : Enum
    {
        var fieldName = Enum.GetName(typeof(T2), enumValue);
        if (fieldName == null)
            throw new InvalidOperationException($"Value {enumValue} is not defined for enum type {typeof(T2).Name}");

        var attribute = typeof(T2).GetField(fieldName)?.GetCustomAttribute<UpgradeConditionArgumentAttribute<T1>>();
        return (attribute is null ? default : attribute.Arg)!;
    }
    
    public static NextLevelFieldInfoAttribute? GetNextLevelCoordinates(this FieldLevels enumValue)
    {
        var fieldName = Enum.GetName(typeof(FieldLevels), enumValue);
        if (fieldName is null)
            throw new InvalidOperationException($"Value {enumValue} is not defined for UpgradeTypes");

        return typeof(FieldLevels).GetField(fieldName)?.GetCustomAttribute<NextLevelFieldInfoAttribute>();
    }

    #endregion
    
    #region UpgradeInfo
    
    public static UpgradeInfoAttribute? GetUpgradeInfo(this UpgradeTypes enumValue)
    {
        var upgradeInfo = UpgradeInfoCache.Value.GetOrAdd(enumValue, value =>
        {
            var fieldName = Enum.GetName(typeof(UpgradeTypes), value);
            if (fieldName is null)
                throw new InvalidOperationException($"Value {value} is not defined for UpgradeTypes");

            return typeof(UpgradeTypes).GetField(fieldName)?.GetCustomAttribute<UpgradeInfoAttribute>();
        });

        return upgradeInfo;
    }
    
    private static readonly Lazy<ConcurrentDictionary<UpgradeTypes, UpgradeInfoAttribute?>>
        UpgradeInfoCache = new(() => new ConcurrentDictionary<UpgradeTypes, UpgradeInfoAttribute?>(), LazyThreadSafetyMode.PublicationOnly);
    
    #endregion
    
    #region TranslationId
    
    public static string? GetTranslationId<T>(this T enumValue) where T : Enum
    {
        var enumValues = TextIdCache.Value.GetOrAdd(typeof(T), _ => new ConcurrentDictionary<int, string?>());

        var textIdValue = enumValues.GetOrAdd(Convert.ToInt32(enumValue), value =>
        {
            var fieldName = Enum.GetName(typeof(T), value);
            if (fieldName == null)
                throw new InvalidOperationException($"Value {enumValue} is not defined for enum type {typeof(T).Name}");

            return typeof(T).GetField(fieldName)?.GetCustomAttribute<TranslationIdAttribute>()?.TextId;
        });

        return textIdValue;
    }

    private static readonly Lazy<ConcurrentDictionary<Type, ConcurrentDictionary<int, string?>>>
        TextIdCache = new(() => new ConcurrentDictionary<Type, ConcurrentDictionary<int, string?>>(), 
            LazyThreadSafetyMode.PublicationOnly);
    
    #endregion
}