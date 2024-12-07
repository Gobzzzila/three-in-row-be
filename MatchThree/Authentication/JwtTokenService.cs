using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MatchThree.API.Authentication.Interfaces;
using MatchThree.Domain.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace MatchThree.API.Authentication;

public class JwtTokenService(TimeProvider timeProvider, IOptions<JwtSettings> options) : IJwtTokenService
{
    private readonly TimeProvider _timeProvider = timeProvider;
    private readonly JwtSettings _options = options.Value;

    public string GenerateJwtToken(long userId)
    {
#if DEBUG
        var jwtKey = _options.Key;
#else
        var jwtKey = Environment.GetEnvironmentVariable("jwtKey")!;
#endif
        
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(
            issuer: _options.Issuer,
            audience: _options.Audience,
            claims: claims,
            expires: _timeProvider.GetLocalNow().DateTime.AddMinutes(30),       //TODO Move magic number to appsettings
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}