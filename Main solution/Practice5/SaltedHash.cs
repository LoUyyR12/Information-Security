using System;
using System.Security.Cryptography;

namespace Practice5
{
    class SaltedHash
    {
        public static byte[] NewSalt()
        {
            const int saltLength = 32;
            using (var randomNumberGenerator =
            new RNGCryptoServiceProvider())
            {
                var randomNumber = new byte[saltLength];
                randomNumberGenerator.GetBytes(randomNumber);
                return randomNumber;
            }
        }
        private static byte[] Combine(byte[] pass, byte[] salt)
        {
            var ret = new byte[pass.Length + salt.Length];
            Buffer.BlockCopy(pass, 0, ret, 0, pass.Length);
            Buffer.BlockCopy(salt, 0, ret, pass.Length, salt.Length);
            return ret;
        }
        public static byte[] HashPasswordWithSalt(
        byte[] toBeHashed, byte[] salt)
        {
            using (var sha256 = SHA256.Create())
            {
                return sha256.ComputeHash(Combine(toBeHashed,
                salt));
            }
        }
    }
}
