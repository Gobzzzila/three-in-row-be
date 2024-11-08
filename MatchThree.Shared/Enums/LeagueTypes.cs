using MatchThree.Shared.Attributes;
using MatchThree.Shared.Constants;

namespace MatchThree.Shared.Enums;

public enum LeagueTypes
{
    [TranslationId(TranslationConstants.CommonUndefinedTextKey)]
    Undefined = 0,
    
    [TranslationId(TranslationConstants.LeagueShrimpTextKey)]
    Shrimp = 1,
    
    [TranslationId(TranslationConstants.LeagueCrabTextKey)]
    Crab = 2,
    
    [TranslationId(TranslationConstants.LeagueOctopusTextKey)]
    Octopus = 3,

    [TranslationId(TranslationConstants.LeagueFishTextKey)]
    Fish = 4,

    [TranslationId(TranslationConstants.LeagueDolphinTextKey)]
    Dolphin = 5,

    [TranslationId(TranslationConstants.LeagueSharkTextKey)]
    Shark = 6,

    [TranslationId(TranslationConstants.LeagueWhaleTextKey)]
    Whale = 7,

    [TranslationId(TranslationConstants.LeagueHumpbackTextKey)]
    Humpback = 8
}