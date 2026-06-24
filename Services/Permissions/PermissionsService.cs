using AuthService.Data;
using AuthService.Models;

namespace AuthService.Services.Permissions;

public class PermissionsService : IPermissionsService
{
    public PermissionsResponse GetPermissions(string username)
    {
        var user = UserStore.FindByUsername(username);

        if (user is null)
            return new PermissionsResponse
            {
                Success = false,
                Message = "Usuario no encontrado"
            };

        var permissions = PermissionsStore.GetPermissions(username);

        return new PermissionsResponse
        {
            Success = true,
            Username = username,
            Permissions = permissions,
            Message = "Permisos cargados"
        };
    }
}