using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Practice3
{
    class Hashing
    {
        public static string Md5(byte[] dataForHash)
        {
            using (var md5 = MD5.Create())
            {
                return Convert.ToBase64String(md5.ComputeHash(dataForHash));
            }
        }
        public static string Sha1(byte[] toBeHashed)
        {
            using (var sha1 = SHA1.Create())
            {
                return Convert.ToBase64String(sha1.ComputeHash(toBeHashed));
            }
        }
        public static string Sha256(byte[] toBeHashed)
        {
            using (var sha256 = SHA256.Create())
            {
                return Convert.ToBase64String(sha256.ComputeHash(toBeHashed));
            }
        }
        public static string Sha384(byte[] toBeHashed)
        {
            using (var sha384 = SHA384.Create())
            {
                return Convert.ToBase64String(sha384.ComputeHash(toBeHashed));
            }
        }
        public static string Sha512(byte[] toBeHashed)
        {
            using (var sha512 = SHA512.Create())
            {
                return Convert.ToBase64String(sha512.ComputeHash(toBeHashed));
            }
        }
    }
}
