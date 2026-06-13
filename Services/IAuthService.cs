using AuthService.Models;

namespace AuthService.Services;

public interface IAuthService
{
    AuthResponse Login(LoginRequest request);
    AuthResponse ValidateToken(string token);
    AuthResponse RefreshToken(string token);
}
