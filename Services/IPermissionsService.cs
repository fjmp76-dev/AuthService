using AuthService.Models;

namespace AuthService.Services;

public interface IPermissionsService
{
    PermissionsResponse GetPermissions(string username);
}