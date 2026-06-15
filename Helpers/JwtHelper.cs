using AuthService.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthService.Helpers;

public class JwtHelper(IConfiguration config)
{
    public string GenerateToken(User user)
    {
        // 1. La clave secreta — debe ser la misma para generar Y validar
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(config["Jwt:Key"]!));

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        // 2. Claims: datos que van DENTRO del token (no encriptados, solo firmados)
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), // ID único del token
            new Claim(JwtRegisteredClaimNames.Iat,                            // cuándo se emitió
                DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString())
        };

        // 3. Construir el token con expiración de 5 minutos
        var expiration = DateTime.UtcNow.AddMinutes(
            config.GetValue<int>("Jwt:ExpirationMinutes"));

        var token = new JwtSecurityToken(
            issuer: config["Jwt:Issuer"],
            audience: config["Jwt:Audience"],
            claims: claims,
            notBefore: DateTime.UtcNow,    // válido desde ahora
            expires: expiration,            // expira en 5 min
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public ClaimsPrincipal? ValidateToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(config["Jwt:Key"]!);

        try
        {
            var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidIssuer = config["Jwt:Issuer"],
                ValidateAudience = true,
                ValidAudience = config["Jwt:Audience"],
                ValidateLifetime = true,           // valida que no esté expirado
                ClockSkew = TimeSpan.Zero          // sin margen extra — exactamente 5 min
            }, out _);

            return principal;
        }
        catch
        {
            return null; // token inválido o expirado
        }
    }

    public string GenerateRefreshToken()
    {
        // Token aleatorio criptográficamente seguro
        var randomBytes = new byte[64];
        using var rng = System.Security.Cryptography.RandomNumberGenerator.Create();
        rng.GetBytes(randomBytes);
        return Convert.ToBase64String(randomBytes);
    }
}
