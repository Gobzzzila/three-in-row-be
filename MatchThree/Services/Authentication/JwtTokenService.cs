using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MatchThree.Domain.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace MatchThree.API.Services.Authentication;

public class JwtTokenService(IConfiguration configuration) : IJwtTokenService
{
    private readonly IConfiguration _configuration = configuration;

    public string GenerateJwtToken(long userId)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(60),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}