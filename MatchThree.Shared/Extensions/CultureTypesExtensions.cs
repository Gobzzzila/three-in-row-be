using MatchThree.Shared.Enums;

namespace MatchThree.Shared.Extensions;

public static class CultureTypesExtensions
{
    public static string ToAcceptLanguage(this CultureTypes cultureType)
    {
        return cultureType switch
        {
            CultureTypes.En => "en-US",
            CultureTypes.Ru => "ru-RU",
            _ => "en-US"
        };
    }
    
    public static CultureTypes AcceptLanguageToCultureTypes(this string acceptLanguage)
    {
        return acceptLanguage.ToLower() switch
        {
            "ru-ru" => CultureTypes.Ru,
            "en-us" => CultureTypes.En,
            _ => CultureTypes.En
        };
    }
    
    public static CultureTypes ReadableLanguageToCultureTypes(this string acceptLanguage)
    {
        return acceptLanguage.ToLower() switch
        {
            "ru" => CultureTypes.Ru,
            "en" => CultureTypes.En,
            _ => CultureTypes.En
        };
    }
}