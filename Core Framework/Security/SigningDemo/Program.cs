/**
 * Подпись
 */

using System;
using System.Security.Cryptography;
using System.Text;

namespace SigningDemo
{
   class Program
   {
      internal static CngKey AliceKeySignature;
      internal static byte[] AlicePubKeyBlob;

      static void Main()
      {
         CreateKeys();

         byte[] aliceData = Encoding.UTF8.GetBytes("Alice");
         byte[] aliceSignature = CreateSignature(aliceData, AliceKeySignature);
         Console.WriteLine("Alice created sugnature: {0}", Convert.ToBase64String(aliceSignature));

         if (VerifySignature(aliceData, aliceSignature, AlicePubKeyBlob))
         {
            Console.WriteLine("Alice signature verified successfully");
         }
      }

      private static bool VerifySignature(byte[] data, byte[] signature, byte[] publicKey)
      {
         bool returnValue;
         using (CngKey key = CngKey.Import(publicKey, CngKeyBlobFormat.GenericPublicBlob))
         using (var signingAlg = new ECDsaCng(key))
         {
            returnValue = signingAlg.VerifyData(data, signature);
            signingAlg.Clear();
         }

         return returnValue;
      }

      private static byte[] CreateSignature(byte[] data, CngKey key)
      {
         byte[] signature;
         using (var signingAlg = new ECDsaCng(key))
         {
            signature = signingAlg.SignData(data);
            signingAlg.Clear();
         }

         return signature;
      }

      private static void CreateKeys()
      {
         AliceKeySignature = CngKey.Create(CngAlgorithm.ECDsaP256);
         AlicePubKeyBlob = AliceKeySignature.Export(CngKeyBlobFormat.GenericPublicBlob);
      }
   }
}
