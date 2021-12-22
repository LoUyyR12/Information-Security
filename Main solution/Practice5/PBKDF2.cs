using System.Security.Cryptography;

namespace Practice5
{
    class PBKDF2
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
        public static byte[] PassHash(byte[] toBeHashed, byte[] salt, int numberOfRounds)
        {
            using (var rfc2898 = new Rfc2898DeriveBytes(
            toBeHashed, salt, numberOfRounds))
            {
                return rfc2898.GetBytes(20);
            }
        }
    }
}
