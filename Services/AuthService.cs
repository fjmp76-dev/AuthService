using AuthService.Data;
using AuthService.Helpers;
using AuthService.Models;

namespace AuthService.Services;

public class AuthService(JwtHelper jwtHelper) : IAuthService
{
    public AuthResponse Login(LoginRequest request)
    {
        // 1. Buscar usuario
        var user = UserStore.FindByUsername(request.Username);

        if (user is null)
            return new AuthResponse { Success = false, Message = "Usuario no encontrado" };

        // 2. Verificar password con BCrypt
        bool passwordValid = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);

        if (!passwordValid)
            return new AuthResponse { Success = false, Message = "Contraseña incorrecta" };

        // 3. Generar token JWT
        var token = jwtHelper.GenerateToken(user);
        var expiresAt = DateTime.UtcNow.AddMinutes(jwtHelper.ExpirationMinutes);

        return new AuthResponse
        {
            Success = true,
            Token = token,
            Username = user.Username,
            Role = user.Role.ToString(),
            ExpiresAt = expiresAt,
            Message = "Login exitoso"
        };
    }

    public AuthResponse ValidateToken(string token)
    {
        var principal = jwtHelper.ValidateToken(token);

        if (principal is null)
            return new AuthResponse { Success = false, Message = "Token inválido o expirado" };

        return new AuthResponse
        {
            Success = true,
            Username = principal.Identity?.Name ?? "",
            Role = principal.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value ?? "",
            Message = "Token válido"
        };
    }
    public AuthResponse RefreshToken(string token)
    {
        var principal = jwtHelper.ValidateToken(token);

        if (principal is null)
            return new AuthResponse { Success = false, Message = "Token inválido o expirado" };

        // Obtener el username del token actual
        var username = principal.Identity?.Name;
        var user = UserStore.FindByUsername(username!);

        if (user is null)
            return new AuthResponse { Success = false, Message = "Usuario no encontrado" };

        // Generar token nuevo con 5 minutos frescos
        var newToken = jwtHelper.GenerateToken(user);
        var expiresAt = DateTime.UtcNow.AddMinutes(jwtHelper.ExpirationMinutes);

        return new AuthResponse
        {
            Success = true,
            Token = newToken,
            Username = user.Username,
            Role = user.Role.ToString(),
            ExpiresAt = expiresAt,
            Message = "Token renovado"
        };
    }
}