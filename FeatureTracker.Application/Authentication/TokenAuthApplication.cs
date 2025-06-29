using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FeatureTracker.Domain.Model.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace FeatureTracker.Application.Authentication;

public class TokenAuthApplication
{
    #region Properties

    private readonly IConfiguration _configuration;

    #endregion

    #region Constructor

    public TokenAuthApplication(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    #endregion

    #region Methods

    public string GenerateToken(User user, DateTime expires)
    {
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = GenerateClaims(user),
            Expires = expires,
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:JwtSecurityKey"]!)),
                SecurityAlgorithms.HmacSha256Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public async Task<bool> ValidateToken(string token)
    {
        if (string.IsNullOrEmpty(token))
            throw new ArgumentException("Token is empty!");

        var key = Encoding.UTF8.GetBytes(_configuration["Jwt:JwtSecurityKey"]!);

        try
        {
            var tokenHandler = await new JwtSecurityTokenHandler()
                .ValidateTokenAsync(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = _configuration["Jwt:JwtIssuer"],
                    ValidateAudience = true,
                    ValidAudience = _configuration["Jwt:JwtAudience"],
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                });

            return true;
        }
        catch
        {
            return false;
        }
    }

    private ClaimsIdentity GenerateClaims(User user) =>
        new(
        [
            new Claim(nameof(User.Username), user.Username),
            new Claim(ClaimTypes.Name, user.Email),
            new Claim(ClaimTypes.Sid, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iss, _configuration["Jwt:JwtIssuer"]!),
            new Claim(JwtRegisteredClaimNames.Aud, _configuration["Jwt:JwtAudience"]!)
        ]);

    #endregion
}
