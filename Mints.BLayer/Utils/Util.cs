using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Mints.BLayer.Utils
{
    public class Util
    {
        public static string GenerateSHA256String(string inputString)
        {
            var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(inputString);
            var hash = sha256.ComputeHash(bytes);
            return GetStringFromHash(hash);
        }

        public static string GenerateSHA512String(string inputString)
        {
            var sha512 = SHA512.Create();
            var bytes = Encoding.UTF8.GetBytes(inputString);
            var hash = sha512.ComputeHash(bytes);
            return GetStringFromHash(hash);
        }

        private static string GetStringFromHash(byte[] hash)
        {
            var result = new StringBuilder();
            for (var i = 0; i < hash.Length; i++)
            {
                result.Append(hash[i].ToString("X2"));
            }
            return result.ToString();
        }
    }
}
