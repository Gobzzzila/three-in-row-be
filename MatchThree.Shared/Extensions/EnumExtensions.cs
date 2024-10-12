using System.Collections.Concurrent;
using System.Reflection;
using MatchThree.Shared.Attributes;

namespace MatchThree.Shared.Extensions;

public static class EnumExtensions
{
    public static uint? GetUpgradeCost<T>(this T enumValue) where T : Enum
    {
        var fieldName = Enum.GetName(typeof(T), enumValue);
        if (fieldName == null)
            throw new InvalidOperationException($"Value {enumValue} is not defined for enum type {typeof(T).Name}");

        return typeof(T).GetField(fieldName)?.GetCustomAttribute<UpgradeCostAttribute>()?.UpgradeCost;
    }
    
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