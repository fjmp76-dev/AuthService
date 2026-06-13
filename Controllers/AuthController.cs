using AuthService.Models;
using AuthService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IAuthService authService) : ControllerBase
{

    // POST /api/auth/login
    [HttpPost("login")]
    [AllowAnonymous]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = authService.Login(request);

        if (!result.Success)
            return Unauthorized(result);

        return Ok(result);
    }

    // POST /api/auth/validate
    [HttpPost("validate")]
    [AllowAnonymous]
    public IActionResult Validate([FromBody] string token)
    {
        var result = authService.ValidateToken(token);
        return result.Success ? Ok(result) : Unauthorized(result);
    }

    // GET /api/auth/me  ← ruta protegida de prueba
    [HttpGet("me")]
    [Authorize]
    public IActionResult Me()
    {
        var username = User.Identity?.Name;
        var role = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;

        return Ok(new { username, role, message = "Token válido, acceso autorizado" });
    }

    [HttpPost("refresh")]
    [Authorize]  // ← debe tener token válido para renovar
    public IActionResult Refresh()
    {
        // El token viene en el header Authorization: Bearer xxx
        var token = Request.Headers.Authorization.ToString().Replace("Bearer ", "");

        var result = authService.RefreshToken(token);

        if (!result.Success)
            return Unauthorized(result);

        return Ok(result);
    }
}