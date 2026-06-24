using AuthService.Services.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize] // ← requiere token válido
public class PermissionsController(IPermissionsService permissionsService) : ControllerBase
{

    // GET /api/permissions/{username}
    [HttpGet("{username}")]
    public IActionResult GetPermissions(string username)
    {
        // Verificar que solo pida sus propios permisos
        var tokenUsername = User.Identity?.Name;
        if (!tokenUsername!.Equals(username, StringComparison.OrdinalIgnoreCase))
            return Forbid();

        var result = permissionsService.GetPermissions(username);

        if (!result.Success)
            return NotFound(result);

        return Ok(result);
    }
}