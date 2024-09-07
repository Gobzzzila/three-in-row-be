using MatchThree.Shared.Attributes;
using MatchThree.Shared.Constants;

namespace MatchThree.Shared.Enums;

public enum LeagueTypes
{
    [TranslationId(TranslationConstants.UndefinedTextKey)]
    Undefined = 0,
    
    [TranslationId(TranslationConstants.ShrimpLeagueTextKey)]
    Shrimp = 1,
    
    [TranslationId(TranslationConstants.CrabLeagueTextKey)]
    Crab = 2,
    
    [TranslationId(TranslationConstants.OctopusLeagueTextKey)]
    Octopus = 3,

    [TranslationId(TranslationConstants.FishLeagueTextKey)]
    Fish = 4,

    [TranslationId(TranslationConstants.DolphinLeagueTextKey)]
    Dolphin = 5,

    [TranslationId(TranslationConstants.SharkLeagueTextKey)]
    Shark = 6,

    [TranslationId(TranslationConstants.WhaleLeagueTextKey)]
    Whale = 7,

    [TranslationId(TranslationConstants.HumpbackLeagueTextKey)]
    Humpback = 8
}