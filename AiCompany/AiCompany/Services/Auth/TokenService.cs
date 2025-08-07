using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AiCompany.Data.Models;
using Microsoft.IdentityModel.Tokens;

namespace AiCompany.Services.Auth;

public class TokenService : ITokenService
{
    private readonly IConfiguration _config;

    public TokenService(IConfiguration config)
    {
        _config = config;
    }

    public string GenerateJwtToken(Company company)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, company.EnglishName),
            new Claim(ClaimTypes.Email, company.Email),
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
