namespace MatchThree.Shared.Constants;

public static class LinksConstants
{
#if DEBUG
    public const string LinkToFrontEnd = "https://cryptofe-75961.web.app";
#else
    public const string LinkToFrontEnd = "https://pingwin-be08f.web.app";
#endif
    
    public const string RuChannel = "https://t.me/PingWinGame_ru";  //Duplicated data, it's already in the localization 
    public const string EnChannel = "https://t.me/PingWinGame";     //but no time to deal with it
}