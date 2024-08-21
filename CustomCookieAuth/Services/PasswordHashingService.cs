using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace CustomCookieAuth.Services
{
    public static class PasswordHashingService
    {
        public static (string password,string salt) HashPassword(string password)
        {
            var salt = GenerateSalt(); 
            return (Hasher(password, salt), Convert.ToBase64String(salt));
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
