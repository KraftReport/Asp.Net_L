using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using System.Text;

namespace CustomCookieAuth.Services
{
    public static class PasswordHashingService
    {
        public static (string password,string salt) HashPassword(string password)
        {
            var salt = GenerateSalt(); 
            return (Hasher(password, salt), Convert.ToBase64String(salt));
        }

        public static string hashForOnePay(string dataString,string secretString)
        {
            var encoding = new System.Text.UTF8Encoding();
            var secretByte = encoding.GetBytes(secretString);
            var hmac = new HMACSHA256(secretByte);
            hmac.Key = secretByte;
            var hashByte = hmac.ComputeHash(encoding.GetBytes(dataString));
            return ByteArrayToHash(hashByte);
        }

        private static string ByteArrayToHash(byte[] byteArray)
        {
            var hashArray = "0123456789ABCDEF";
            var sBuilder = new StringBuilder(byteArray.Length * 2);
            foreach(var b in byteArray)
            {
                sBuilder.Append(hashArray[(int)b >> 4]);
                sBuilder.Append(hashArray[(int)b & 0xF]);
            }
            return sBuilder.ToString();
        }

        public static bool VerifyPassword(string enteredPassword,string storedHash,string storedSalt)
        {
            var salt = Convert.FromBase64String(storedSalt);

            var hash = Hasher(enteredPassword, salt);

            return hash == storedHash;
        }

        private static string Hasher(string password, byte[] salt)
        {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
        }


        private static byte[] GenerateSalt()
        {
            var salt = new byte[128 / 8];
            var rng = RandomNumberGenerator.Create();
            rng.GetBytes(salt);
            return salt;
        }
    }

    
}
