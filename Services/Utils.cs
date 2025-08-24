using System.Security.Cryptography;
using System.Text;

namespace AuthService.Utils;

public class Helpers
{
    public static string HashPassword(string password)
    {
        using var sha = SHA256.Create();
        var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(bytes);
    }

    public static bool VerifyPassword(string providedPassword, string hashPassword)
    {
        return hashPassword == HashPassword(providedPassword);
    }
}