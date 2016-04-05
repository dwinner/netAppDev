/**
 * Обмен ключами и безопасная передача данных
 */

using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SecureTransfer
{
   class Program
   {
      internal static CngKey AliceKey;
      internal static CngKey BobKey;
      internal static byte[] AlicePubKeyBlob;
      internal static byte[] BobPubKeyBlob;

      static void Main()
      {
         Run();
         Console.ReadLine();
      }

      private async static void Run()
      {
         try
         {
            CreateKeys();
            byte[] encryptedData = await AliceSendsData("secret message");
            await BobReceivesData(encryptedData);
         }
         catch (Exception ex)
         {
            Console.WriteLine(ex.Message);
         }
      }

      private async static Task BobReceivesData(byte[] encryptedData)
      {
         await Task.Run(() =>
         {
            Console.WriteLine("Bob receives encrypted data");
            var aes = new AesCryptoServiceProvider();
            int nBytes = aes.BlockSize >> 3;
            var iv = new byte[nBytes];
            for (int i = 0; i < iv.Length; i++)
            {
               iv[i] = encryptedData[i];
            }
            using (var bobAlg = new ECDiffieHellmanCng(BobKey))
            using (CngKey alicePubKey = CngKey.Import(AlicePubKeyBlob, CngKeyBlobFormat.EccPublicBlob))
            {
               byte[] symmKey = bobAlg.DeriveKeyMaterial(alicePubKey);
               Console.WriteLine("Bob creates this symmetric key with Alices public key information: {0}",
                  Convert.ToBase64String(symmKey));
               aes.Key = symmKey;
               aes.IV = iv;
               using (ICryptoTransform decryptor = aes.CreateDecryptor())
               using (var memoryStream = new MemoryStream())
               {
                  var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Write);
                  cryptoStream.Write(encryptedData, nBytes, encryptedData.Length - nBytes);
                  cryptoStream.Close();
                  byte[] rawData = memoryStream.ToArray();
                  Console.WriteLine("Bob decrypts message to: {0}", Encoding.UTF8.GetString(rawData));
               }
               aes.Clear();
            }
         });
      }

      private static async Task<byte[]> AliceSendsData(string message)
      {
         Console.WriteLine("Alice sends message: {0}", message);
         byte[] rawData = Encoding.UTF8.GetBytes(message);
         byte[] encryptedData;
         using (var aliceAlg = new ECDiffieHellmanCng(AliceKey))
         using (CngKey bobPubKey = CngKey.Import(BobPubKeyBlob, CngKeyBlobFormat.EccPublicBlob))
         {
            byte[] symmKey = aliceAlg.DeriveKeyMaterial(bobPubKey);
            Console.WriteLine("Alice creates this symmetric key with Bobs public key information: {0}",
               Convert.ToBase64String(symmKey));
            using (var aes = new AesCryptoServiceProvider { Key = symmKey })
            {
               aes.GenerateIV();
               using (ICryptoTransform encryptor = aes.CreateEncryptor())
               using (var memoryStream = new MemoryStream())
               {
                  var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);  // Создать CryptoStream и зашифровать отправляемые данные.                  
                  await memoryStream.WriteAsync(aes.IV, 0, aes.IV.Length); // Записать вектор инициализации, не шифруя его
                  cryptoStream.Write(rawData, 0, rawData.Length);
                  cryptoStream.Close();
                  encryptedData = memoryStream.ToArray();
               }
               aes.Clear();
            }
         }
         Console.WriteLine("Alice: message is encrypted: {0}", Convert.ToBase64String(encryptedData));
         Console.WriteLine();

         return encryptedData;
      }

      private static void CreateKeys()
      {
         AliceKey = CngKey.Create(CngAlgorithm.ECDiffieHellmanP256);
         BobKey = CngKey.Create(CngAlgorithm.ECDiffieHellmanP256);
         AlicePubKeyBlob = AliceKey.Export(CngKeyBlobFormat.EccPublicBlob);
         BobPubKeyBlob = BobKey.Export(CngKeyBlobFormat.EccPublicBlob);
      }
   }
}
