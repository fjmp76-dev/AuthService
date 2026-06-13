using AuthService.Models;

namespace AuthService.Data;

public static class UserStore
{
    public static List<User> Users { get; } =
    [
        new User
        {
            Id = 1,
            Username = "francisco",
            Email = "francisco@example.com",
            // BCrypt hash de "Password123"
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Password123"),
            Role = "Admin"
        },
        new User
        {
            Id = 2,
            Username = "testuser",
            Email = "test@example.com",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Test456"),
            Role = "User"
        }
    ];

    public static User? FindByUsername(string username) =>
        Users.FirstOrDefault(u =>
            u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
}