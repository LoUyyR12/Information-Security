using System.Security.Cryptography;
using System.Text;

namespace Practice3
{
    class Hashing
    {

        public static byte[] Md5(byte[] Data)
        {
            using (var md5 = MD5.Create())
            {
                return md5.ComputeHash(Data);
            }
        }
        public class Sha1
        {
            public static string Hash(byte[] Data)
            {
                using (var sha1 = SHA1.Create())
                {
                    return Encoding.UTF8.GetString(sha1.ComputeHash(Data));
                }
            }
            public static byte[] HMAC(byte[] Data, byte[] key)
            {
                using (var hmac = new HMACSHA1(key))
                {
                    return hmac.ComputeHash(Data);
                }
            }
        }
        public static string Sha256(byte[] Data)
        {
            using (var sha256 = SHA256.Create())
            {
                return Encoding.UTF8.GetString(sha256.ComputeHash(Data));
            }
        }
        public static string Sha384(byte[] Data)
        {
            using (var sha384 = SHA384.Create())
            {
                return Encoding.UTF8.GetString(sha384.ComputeHash(Data));
            }
        }
        public static string Sha512(byte[] Data)
        {
            using (var sha512 = SHA512.Create())
            {
                return Encoding.UTF8.GetString(sha512.ComputeHash(Data));
            }
        }
    }
}
