namespace MatchThree.API.Authentication.Interfaces;

public interface IJwtTokenService
{
    string GenerateJwtToken(long userId);
}