﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AuthenticationService.Data.Configuration;
using AuthenticationService.Data.Storage;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AuthenticationService.Utilities.Jwt;

public class JwtUtils : IJwtUtils
{
    private readonly byte[] _secretKey;

    public JwtUtils(IOptions<AppSettings> appSettings)
    {
        if (string.IsNullOrEmpty(appSettings.Value.TokenSignSecret))
            throw new Exception("JWT secret not configured");

        _secretKey = Encoding.ASCII.GetBytes(appSettings.Value.TokenSignSecret);
    }

    public string GenerateJwtToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] {
                new Claim("id", user.Id.ToString()),
                new Claim("name", user.Name),
                new Claim("role", user.Role.ToString()) }),
            Expires = DateTime.UtcNow.AddMinutes(15),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(_secretKey), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
