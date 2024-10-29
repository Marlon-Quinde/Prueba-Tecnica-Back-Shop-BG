using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Helpers
{
    public static class Encrypter
    {
        private static char Splitter = ';';

        public static byte[] GenerateSalt()
        {
            var salt = new byte[16];
            var rng = RandomNumberGenerator.Create();
            rng.GetBytes(salt);

            return salt;
        }

        public static string HashPassword(string password)
        {
            var Salt = GenerateSalt();
            byte[] Stringbytes = Encoding.UTF8.GetBytes(password);
            var HashedPassword = SHA256.HashData(Stringbytes);

            return Convert.ToBase64String(HashedPassword) + Splitter + Convert.ToBase64String(Salt);
        }


        public static string GetSalt(string HSPassword)
        {
            var Salt = HSPassword.Split(Splitter)[1];

            return Salt;
        }

        public static bool CompareHash(string password, string salt, string HSPassword)
        {
            bool Check = true;
            byte[] Stringbytes = Encoding.UTF8.GetBytes(password);
            var HashedPassword = SHA256.HashData(Stringbytes);

            var compare = Convert.ToBase64String(HashedPassword) + Splitter + salt;
            if (compare == HSPassword)
            {
                return Check;
            }
            else
            {
                Check = false;
                return Check;
            }

        }

        public static bool ValidatePassword(string password, string HSPassword)
        {
            byte[] Stringbytes = Encoding.UTF8.GetBytes(password);
            var HashedPassword = SHA256.HashData(Stringbytes);
            var salt = GetSalt(HSPassword);
            var compare = Convert.ToBase64String(HashedPassword) + Splitter + salt;

            return compare == HSPassword;
        }
    }
}
