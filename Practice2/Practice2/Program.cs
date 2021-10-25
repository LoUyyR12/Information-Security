using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Practice2
{
    class Program
    {
        static void Main(string[] args)
        {
            GenerateKey();
            Encrypting();
            Decoding();
            Decryption();
        }
        private static void GenerateKey()
        {
            byte[] Data = File.ReadAllBytes("input.txt").ToArray();
            byte[] key = Crypto.GetCryptoRng(Data.Length);
            File.WriteAllBytes("key.dat", key);
        }

        private static void Encrypting()
        {
            byte[] decData = File.ReadAllBytes("input.txt").ToArray();
            byte[] key = File.ReadAllBytes("key.dat").ToArray();
            byte[] encData = new byte[decData.Length];
            for (int i = 0; i < decData.Length; i++)
            {
                encData[i] = (byte)(decData[i] ^ key[i]);
            }
            File.WriteAllBytes("encFile.dat", encData);
        }
        private static void Decoding()
        {
            byte[] encData = File.ReadAllBytes("encFile.dat").ToArray();
            byte[] key = File.ReadAllBytes("key.dat").ToArray();
            byte[] decData = new byte[encData.Length];
            for (int i = 0; i < encData.Length; i++)
            {
                decData[i] = (byte)(encData[i] ^ key[i]);
            }
            string text = Encoding.UTF8.GetString(decData);
            File.WriteAllText("decodedFile.txt", text);
        }
        private static void Decryption()
        {
            while (true) { 
                byte[] encData = File.ReadAllBytes("encfile5.dat").ToArray();
                byte[] key = Crypto.GetCryptoRng(5);
                byte[] decData = new byte[encData.Length];
                for (int i = 0; i < encData.Length; i++)
                {
                    int j = i % 5;
                    decData[i] = (byte)(encData[i] ^ key[j]);
                }
                string text = Encoding.UTF8.GetString(decData);
                if (text.Contains("Міт21"))
                {
                    File.WriteAllBytes("TeacherKey.dat", key);
                    Console.WriteLine(text);
                    break;
                }
            }
        }
    }
}
