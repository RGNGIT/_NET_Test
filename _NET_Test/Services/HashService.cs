using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace _NET_Test.Services
{
    public class HashService
    {
        public string CreateHash(string value)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(128 / 8);
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: value!,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 5928,
                numBytesRequested: 256 / 8));
            return hashed;
        }
    }
}
