namespace AuthService.Models;

public class PermissionsResponse
{
    public bool Success { get; set; }
    public string Username { get; set; } = string.Empty;
    public List<Permission> Permissions { get; set; } = new();
    public string Message { get; set; } = string.Empty;
}