namespace MatchThree.Domain.Interfaces;

public interface IJwtTokenService
{
    string GenerateJwtToken(long userId);
}