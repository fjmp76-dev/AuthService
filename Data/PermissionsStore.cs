using AuthService.Enums;
using AuthService.Models;

namespace AuthService.Data;

public static class PermissionsStore
{
    public static List<(string Username, List<Permission> Permissions)> UserPermissions { get; } =
    [
        (
            "francisco",
            new List<Permission>
            {
                new() { Screen = "landing",   Level = PermissionLevel.W },
                new() { Screen = "captura",   Level = PermissionLevel.W },
                new() { Screen = "permisos",  Level = PermissionLevel.W }
            }
        ),
        (
            "testuser",
            new List<Permission>
            {
                new() { Screen = "landing",   Level = PermissionLevel.R },
                new() { Screen = "captura",   Level = PermissionLevel.R },
                new() { Screen = "permisos",  Level = PermissionLevel.N }
            }
        ),
        (
            "testpato",
            new List<Permission>
            {
                new() { Screen = "dashboard", Level = PermissionLevel.R },
                new() { Screen = "captura",   Level = PermissionLevel.N },
                new() { Screen = "permisos",  Level = PermissionLevel.N }
            }
        )
        ];

    public static List<Permission> GetPermissions(string username) =>
        UserPermissions
            .FirstOrDefault(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase))
            .Permissions ?? [];
}