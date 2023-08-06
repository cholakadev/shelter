using System.Security.Cryptography;
using BCryptNet = BCrypt.Net.BCrypt;

namespace Users.Core.Helpers
{
    public static class PasswordHelper
    {
        public static string HashPassword(string password, out string salt, int workFactor = 12)
        {
            salt = BCryptNet.GenerateSalt(workFactor);

            string hashedPassword = BCryptNet.HashPassword(password, salt);
            return hashedPassword;
        }
    }
}
