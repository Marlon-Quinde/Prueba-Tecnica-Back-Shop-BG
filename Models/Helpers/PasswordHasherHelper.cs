using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Models.Helpers
{
    public static class PasswordHasherHelper
    {
        private static readonly string _secretKey;
        static PasswordHasherHelper()
        {
            _secretKey = Environment.GetEnvironmentVariable("Token_Key");
        }

        public static string HashPassword(string password)
        {
            using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(_secretKey)))
            {
                var passwordBytes = Encoding.UTF8.GetBytes(password);
                var hashedBytes = hmac.ComputeHash(passwordBytes);
                return Convert.ToBase64String(hashedBytes);
            }
        }

        public static bool VerifyPassword(string hashedPassword, string providedPassword)
        {
            var hashedBytes = Convert.FromBase64String(hashedPassword);
            using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(_secretKey)))
            {
                var providedPasswordBytes = Encoding.UTF8.GetBytes(providedPassword);
                var hashedProvidedBytes = hmac.ComputeHash(providedPasswordBytes);
                return hashedBytes.SequenceEqual(hashedProvidedBytes);
            }
        }
    }
}
