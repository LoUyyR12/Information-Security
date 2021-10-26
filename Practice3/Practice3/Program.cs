using System;
using System.IO;
using System.Linq;
using System.Text;

namespace Practice3
{
    class Program
    {
        static void Main(string[] args)
        {
            byte[] Data = File.ReadAllBytes("data.dat").ToArray();
            Console.WriteLine("Hashing with Md5:");
            Console.WriteLine(Hashing.Md5(Data));
            Console.WriteLine("Hashing with SHA1:");
            Console.WriteLine(Hashing.Sha1(Data));
            Console.WriteLine("Hashing with SHA256:");
            Console.WriteLine(Hashing.Sha256(Data));
            Console.WriteLine("Hashing with SHA384:");
            Console.WriteLine(Hashing.Sha384(Data));
            Console.WriteLine("Hashing with SHA512:");
            Console.WriteLine(Hashing.Sha512(Data));

            Guid targetGuid = new Guid("564c8da6-0440-88ec-d453-0bbad57c6036");
            string targetHash = "po1MVkAE7IjUUwu61XxgNg==";
            //for (int i = 100000000; i > 10000000; i--)
            //{
            //    if (Hashing.Md5(Encoding.UTF8.GetBytes(i.ToString())) == targetHash)
            //    {
            //        Console.WriteLine(i);
            //    }
            //}
            string[] possibleNumbers = new string[10];
            for (int i = 0; i < 10; i++)
            {
                possibleNumbers[i] = i.ToString();
            }
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(possibleNumbers[i]);
            }
            Console.WriteLine(Decoding(targetHash, possibleNumbers, 8));
        }
        static string Decoding(string targetHash, string[] possibleValues, int passwordLength)
        {
            string[] password = null;
            Decoding_Loop(password, possibleValues, passwordLength, targetHash);
            return "end";
        }
        static void Decoding_Loop(string[] password, string[] possibleValues, int passwordLength, string targetHash)
        {
            if (password != null)
            {
                if (password.Length == passwordLength)
                {
                    string strPassword = null;
                    foreach (string Symbol in password)
                    {
                        strPassword += Symbol;
                    }
                        if (Hashing.Md5(Encoding.UTF8.GetBytes(strPassword)) == targetHash)
                    {
                        Console.WriteLine(password);
                    }
                    Console.WriteLine(password);
                }
                return;
            }
            foreach (string Symbol in possibleValues)
            {
                password[password.Length] = Symbol;
                Decoding_Loop(password, possibleValues, passwordLength, targetHash);
            }
            return;
        }
            
    }
}
