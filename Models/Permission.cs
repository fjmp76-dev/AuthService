using AuthService.Enums;

namespace AuthService.Models;

public class Permission
{
    public string Screen { get; set; } = string.Empty;
    public PermissionLevel Level { get; set; } = PermissionLevel.N;
}