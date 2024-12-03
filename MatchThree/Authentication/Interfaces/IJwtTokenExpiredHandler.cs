namespace MatchThree.API.Authentication.Interfaces;

public interface IJwtTokenExpiredHandler
{
    void HandleAsync(HttpContext context);
}