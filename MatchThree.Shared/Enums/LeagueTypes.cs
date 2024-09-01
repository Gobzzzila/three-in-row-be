using MatchThree.Shared.Attributes;
using MatchThree.Shared.Constants;

namespace MatchThree.Shared.Enums;

public enum LeagueTypes
{
    [TranslationId(TranslationConstants.UndefinedTextId)]
    Undefined = 0,
    
    [TranslationId(TranslationConstants.ShrimpLeagueTextId)]
    Shrimp = 1,
    
    [TranslationId(TranslationConstants.CrabLeagueTextId)]
    Crab = 2,
    
    [TranslationId(TranslationConstants.OctopusLeagueTextId)]
    Octopus = 3,

    [TranslationId(TranslationConstants.FishLeagueTextId)]
    Fish = 4,

    [TranslationId(TranslationConstants.DolphinLeagueTextId)]
    Dolphin = 5,

    [TranslationId(TranslationConstants.SharkLeagueTextId)]
    Shark = 6,

    [TranslationId(TranslationConstants.WhaleLeagueTextId)]
    Whale = 7,

    [TranslationId(TranslationConstants.HumpbackLeagueTextId)]
    Humpback = 8
}