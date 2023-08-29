using System.IdentityModel.Tokens.Jwt;
using System.Text;
using AccountingService.Data.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AccountingService.Utilities.Jwt;

public class JwtUtils : IJwtUtils
{
    private readonly byte[] _secretKey;

    public JwtUtils(IOptions<AppSettings> appSettings)
    {
        if (string.IsNullOrEmpty(appSettings.Value.TokenSignSecret))
            throw new Exception("JWT secret not configured");

        _secretKey = Encoding.ASCII.GetBytes(appSettings.Value.TokenSignSecret);
    }

    public Guid? ValidateJwtToken(string? token)
    {
        if (token == null)
            return null;

        var tokenHandler = new JwtSecurityTokenHandler();

        try
        {
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(_secretKey),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            var accountId = Guid.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);

            return accountId;
        }
        catch
        {
            return null;
        }
    }
}
