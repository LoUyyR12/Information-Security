using System;
using System.Security.Cryptography;

namespace Practice2
{
    class Crypto
    {
        static public byte[] GetCryptoRng(int length = 10)
        {
            var rngGen = new RNGCryptoServiceProvider();
            var rndNumber = new byte[length];
            rngGen.GetBytes(rndNumber);
            return rndNumber;
        }
    }
}
