using System.Security.Cryptography;

namespace CryptoRng
{
    public class Crypto
    {
        static void Main()
        {

        }
        static public byte[] GetCryptoRng(int length = 10)
        {
            var rngGen = new RNGCryptoServiceProvider();
            var rndNumber = new byte[length];
            rngGen.GetBytes(rndNumber);
            return rndNumber;
        }
    }
}
