using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationAPI.Core.Cryptography
{
    public static class Hmac
    {
        public static string HashHMACHex(byte[] key, string messageHex)
        {
            var message = Convert.FromHexString(messageHex);
            var hash = new HMACSHA256(key);
            return HashEncode(hash.ComputeHash(message));
        }

        private static string HashEncode(byte[] hash)
        {
            return BitConverter.ToString(hash).Replace("-", "").ToLower();
        }

        public static string Digest(string msg, byte[] key, string alg = "HmacSHA256")
        {
            var signingKey = new System.Security.Cryptography.HMACSHA256(key);
            var bytes = signingKey.ComputeHash(Encoding.UTF8.GetBytes(msg));
            return Format(bytes);
        }

        private static string Format(byte[] bytes)
        {
            var builder = new StringBuilder();
            foreach (var b in bytes)
            {
                builder.AppendFormat("{0:x2}", b);
            }
            return builder.ToString();
        }
    }
}
