using CryptoRng;
using System;
using System.Security.Cryptography;

namespace Practice6
{
    class PBKDF2
    {
        public static byte[] NewSalt()
        {
            return Crypto.GetCryptoRng(32);
        }
        public static byte[] HashPassword(byte[] toBeHashed, byte[] salt, int numberOfRounds, HashAlgorithmName hashAlgorithm, Int32 NumberOfBytes)
        {
            using (var rfc2898 = new Rfc2898DeriveBytes(toBeHashed, salt, numberOfRounds, hashAlgorithm))
            {
                return rfc2898.GetBytes(NumberOfBytes);
            }
        }
    }
}
