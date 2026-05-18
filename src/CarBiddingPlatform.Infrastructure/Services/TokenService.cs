using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CarBiddingPlatform.Application.DTOs;
using CarBiddingPlatform.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace CarBiddingPlatform.Infrastructure.Services;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public string GenerateToken(TokenUserInfo data)
    {
        // Get the settings
        var jwtSettings = _configuration.GetSection("JwtSettings");
        var secretKey = Encoding.UTF8.GetBytes(jwtSettings["Secret"]!);

        // Create the claims 
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, data.UserId),
            new Claim(JwtRegisteredClaimNames.Email, data.Email),
            new Claim(JwtRegisteredClaimNames.Name, data.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) // Unique ID for this specific token
        };

        // Create the lock and key
        var key = new SymmetricSecurityKey(secretKey);
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

        // Describe the token
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(2), //expiration time
            Issuer = jwtSettings["Issuer"],
            Audience = jwtSettings["Audience"],
            SigningCredentials = credentials
        };

        // Generate and return the string
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        
        return tokenHandler.WriteToken(token);
    }
}