namespace MatchThree.Domain.Settings;

public class JwtSettings
{
    public required string Key { get; set; }
    public required string Issuer { get; init; }
    public required string Audience { get; init; }
}