using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Practice9
{
    class Keys
    {
        public static void DeleteKeyInCsp()
        {
            CspParameters cspParameters = new CspParameters
            {
                KeyContainerName = Program.GetCspContainerName(),
                Flags = CspProviderFlags.UseMachineKeyStore
            };
            var rsa = new RSACryptoServiceProvider(cspParameters)
            {
                PersistKeyInCsp = false
            };
            rsa.Clear();
        }
        public static void AssignNewKey(string publicKeyPath)
        {
            CspParameters cspParameters = new CspParameters(1)
            {
                KeyContainerName = Program.GetCspContainerName(),
                Flags = CspProviderFlags.UseMachineKeyStore,
                ProviderName = "Microsoft Strong Cryptographic Provider"
            };
            using (var rsa = new RSACryptoServiceProvider(cspParameters))
            {
                rsa.PersistKeyInCsp = true;
                File.WriteAllText(publicKeyPath, rsa.ToXmlString(false));
            }
        }
    }
}
