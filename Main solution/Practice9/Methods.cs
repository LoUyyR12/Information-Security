using System.IO;
using System.Security.Cryptography;

namespace Practice9
{
    class Methods
    {
        private readonly static string CspContainerName = "RsaContainer";
        public static void DeleteKeyInCsp()
        {
            CspParameters cspParameters = new CspParameters
            {
                KeyContainerName = CspContainerName,
                Flags = CspProviderFlags.UseMachineKeyStore
            };
            var rsa = new RSACryptoServiceProvider(2048, cspParameters)
            {
                PersistKeyInCsp = false
            };
            rsa.Clear();
        }
        public static byte[] CreateSign(string publicKeyPath, byte[] dataToSign)
        {
            var cspParams = new CspParameters
            {
                KeyContainerName = CspContainerName,
                Flags = CspProviderFlags.UseMachineKeyStore
            };
            using (var rsa = new RSACryptoServiceProvider(2048, cspParams))
            {
                rsa.PersistKeyInCsp = true;
                File.WriteAllText(publicKeyPath, rsa.ToXmlString(false));
                var rsaFormatter = new RSAPKCS1SignatureFormatter(rsa);
                rsaFormatter.SetHashAlgorithm(nameof(SHA512));
                byte[] hashOfData;
                using (var sha512 = SHA512.Create())
                {
                    hashOfData = sha512.ComputeHash(dataToSign);
                }
                return rsaFormatter.CreateSignature(hashOfData);

            }
        }
        public static bool VerifySignature(string publicKeyPath, byte[] data, byte[] signature)
        {
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.PersistKeyInCsp = false;
                rsa.FromXmlString(File.ReadAllText(publicKeyPath));
                var rsaDeformatter = new RSAPKCS1SignatureDeformatter(rsa);
                rsaDeformatter.SetHashAlgorithm(nameof(SHA512));
                byte[] hashOfData;
                using (var sha512 = SHA512.Create())
                {
                    hashOfData = sha512.ComputeHash(data);

                }
                return rsaDeformatter.VerifySignature(hashOfData, signature);

            }
        }
    }
}
