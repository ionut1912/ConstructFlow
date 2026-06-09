using ConstructFlow.Auth.Domain.Abstractions.Services;
using ConstructFlow.Auth.Domain.Entities;
using ConstructFlow.Auth.Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ConstructFlow.Auth.Infrastructure.Services;

public class TokenService(IConfiguration configuration) : ITokenService
{
    public AuthResult GenerateToken(Account account)
    {
        IConfigurationSection jwtSettings = configuration.GetSection("JwtSettings");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        string jti = Guid.NewGuid().ToString();


        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, account.Id.ToString()),
            new(JwtRegisteredClaimNames.Name, account.Username),
            new(JwtRegisteredClaimNames.Email, account.Email),
            new(JwtRegisteredClaimNames.Jti, jti),
            new(ClaimTypes.Role,account.Role.Value)
        };


        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = jwtSettings["Issuer"],
            Audience = jwtSettings["Audience"],
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(2),
            SigningCredentials = creds
        };


        var handler = new JsonWebTokenHandler();
        string? tokenString = handler.CreateToken(tokenDescriptor);

        return new AuthResult(tokenString, jti);
    }

    public RefreshToken GenerateRefreshToken(Guid userId, string jwtToken)
    {
        var handler = new JsonWebTokenHandler();
        JsonWebToken? jwt = handler.ReadJsonWebToken(jwtToken);
        string jti = jwt.GetClaim(JwtRegisteredClaimNames.Jti).Value; // extracts the 36-char GUID

        return RefreshToken.Create(userId, GenerateRandomString(35), jti, DateTime.UtcNow.AddMonths(6));
    }

    private static string GenerateRandomString(int length)
    {
        byte[] random = new byte[length];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(random);
        return Convert.ToBase64String(random);
    }
}