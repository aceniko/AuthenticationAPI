using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationAPI.Core.Cryptography
{
    public static class SecretGenerator
    {
        public static string GenerateActivationCode() {
            Random rnd = new Random();
            return rnd.Next(10000000, 99999999).ToString();
        }

        public static Tuple<byte[], byte[]> GenerateSecret(byte[] otherPublicKey)
        {
            using (ECDiffieHellmanCng ecdh = new ECDiffieHellmanCng(ECCurve.CreateFromFriendlyName("secp384r1")) { HashAlgorithm = CngAlgorithm.Sha384 })
            {
                var publicKeyBytes = ecdh.ExportSubjectPublicKeyInfo();

                var otherEcdh = new ECDiffieHellmanCng(ECCurve.CreateFromFriendlyName("secp384r1"));
                otherEcdh.ImportSubjectPublicKeyInfo(otherPublicKey, out _);
                var sharedKey = ecdh.DeriveKeyMaterial(otherEcdh.PublicKey);
                return new Tuple<byte[], byte[]>(publicKeyBytes, sharedKey);
            }
        }
       

    }
}
