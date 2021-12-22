using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Practice9
{
    class Encryption
    {
        public static byte[] EncryptData(byte[] dataToEncrypt, string path)
        {
            byte[] cypherBytes;
            using (var rsa = new RSACryptoServiceProvider())
            {
                rsa.FromXmlString(File.ReadAllText(path));
                cypherBytes = rsa.Encrypt(dataToEncrypt, true);
            }
            return cypherBytes;
        }

        public static byte[] DecryptData(byte[] dataToDecrypt)
        {
            byte[] plainBytes;
            var cspParams = new CspParameters
            {
                KeyContainerName = Program.GetCspContainerName(),
                Flags = CspProviderFlags.UseMachineKeyStore
            };
            using (var rsa = new RSACryptoServiceProvider(cspParams))
            {
                rsa.PersistKeyInCsp = true;
                plainBytes = rsa.Decrypt(dataToDecrypt, true);
            }
            return plainBytes;
        }
    }
}
