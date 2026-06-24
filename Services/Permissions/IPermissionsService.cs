using AuthService.Models;

namespace AuthService.Services.Permissions;

public interface IPermissionsService
{
    PermissionsResponse GetPermissions(string username);
}