using System.Security.Cryptography;
using System.Text;

namespace AESInMemoryCrypto;

internal class MemCrypt
{
#if NET6_0_OR_GREATER
   // From .NET 6, you can use the EncryptCbc method to shortcut the process:
   public static byte[] Encrypt(byte[] data, byte[] key, byte[] iv)
   {
      using var algorithm = Aes.Create();
      algorithm.Key = key;
      return algorithm.EncryptCbc(data, iv);
   }

   // From .NET 6, we can use the DecryptCbc method to shortcut the process:
   public static byte[] Decrypt(byte[] data, byte[] key, byte[] iv)
   {
      using var algorithm = Aes.Create();
      algorithm.Key = key;
      return algorithm.DecryptCbc(data, iv);
   }
#else
      public static byte[] Encrypt(byte[] data, byte[] key, byte[] iv)
      {
         using (Aes algorithm = Aes.Create())
         using (ICryptoTransform encryptor = algorithm.CreateEncryptor(key, iv))
            return Crypt(data, encryptor);
      }

      public static byte[] Decrypt(byte[] data, byte[] key, byte[] iv)
      {
         using (Aes algorithm = Aes.Create())
         using (ICryptoTransform decryptor = algorithm.CreateDecryptor(key, iv))
            return Crypt(data, decryptor);
      }

      static byte[] Crypt(byte[] data, ICryptoTransform cryptor)
      {
         MemoryStream m = new MemoryStream();
         using (Stream c = new CryptoStream(m, cryptor, CryptoStreamMode.Write))
            c.Write(data, 0, data.Length);
         return m.ToArray();
      }
#endif

   // Additional overloads that accept strings:

   public static string Encrypt(string data, byte[] key, byte[] iv) =>
      Convert.ToBase64String(
         Encrypt(Encoding.UTF8.GetBytes(data), key, iv));

   public static string Decrypt(string data, byte[] key, byte[] iv) =>
      Encoding.UTF8.GetString(
         Decrypt(Convert.FromBase64String(data), key, iv));
}