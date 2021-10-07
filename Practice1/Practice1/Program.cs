using System;
using System.Security.Cryptography;

namespace Practice1
{
    class Program
    {
        static byte[] GetCryptoRng(int length = 10)
        {
            var rngGen = new RNGCryptoServiceProvider();
            var rndNumber = new byte[length];
            rngGen.GetBytes(rndNumber);
            return rndNumber;
        }
        static void Main(string[] args)
        {
            Random random = new Random(3247);
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(random.Next(-100, 100));
            }
            Console.WriteLine("-------------------------");
            Random random1 = new Random(3247);
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(random1.Next(-100, 100));
            }
            Console.WriteLine("-------------------------");
            for (int i = 0; i < 4; i++)
            {
                string textRNGs = Convert.ToBase64String(GetCryptoRng());
                Console.WriteLine(textRNGs);
            }
        }
        
    }
}
