using System;
using CryptoRng;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Practice6
{
    class Program
    {
        static void Main(string[] args)
        {
             // testing
            var key = Crypto.GetCryptoRng(32);
            Console.WriteLine(key);
            var initVector = Crypto.GetCryptoRng(16);
            Console.WriteLine(initVector);
            const string input = "Some text";
            var encryptedInput = Encrypt(Encoding.UTF8.GetBytes(input), key, initVector);
            var decryptedInput = Encoding.UTF8.GetString(Decrypt(encryptedInput, key, initVector));
        }

        public static byte[] Encrypt(byte[] Data, byte[] key, byte[] initVector)
        {
            using (var aes = new AesCryptoServiceProvider())
            {
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
                aes.Key = key;
                aes.IV = initVector;
                using (var memoryStream = new MemoryStream())
                {
                    var cryptoStream = new CryptoStream
                        (
                        memoryStream,
                        aes.CreateEncryptor(),
                        CryptoStreamMode.Write
                        );
                    cryptoStream.Write(Data, 0, Data.Length);
                    cryptoStream.FlushFinalBlock();
                    return memoryStream.ToArray();
                }
            }
        }
        public static byte[] Decrypt(byte[] Data, byte[] key, byte[] initVector)
        {
            using (var aes = new AesCryptoServiceProvider())
            {
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
                aes.Key = key;
                aes.IV = initVector;
                using (var memoryStream = new MemoryStream())
                {
                    var cryptoStream = new CryptoStream
                        (
                        memoryStream,
                        aes.CreateDecryptor(),
                        CryptoStreamMode.Write
                        );
                    cryptoStream.Write(Data, 0, Data.Length);
                    cryptoStream.FlushFinalBlock();
                    return memoryStream.ToArray();
                }
            }
        }
    }
}
