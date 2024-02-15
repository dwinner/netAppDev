using System.Security.Cryptography;
using System.Text;

namespace AESInMemoryCrypto;

internal static class Program
{
   private static void Main()
   {
      var key = new byte[16];
      var iv = new byte[16];

      var cryptoRng = RandomNumberGenerator.Create();
      cryptoRng.GetBytes(key);
      cryptoRng.GetBytes(iv);

      Console.WriteLine($"Key: {string.Join("", key.Select(b => b.ToString("x2")))}");
      Console.WriteLine($"IV : {string.Join("", iv.Select(b => b.ToString("x2")))}");

      var toEncrypt = "There are 10 kinds of people. Those that understand binary, and those that don't.";
      var encrypted = MemCrypt.Encrypt(Encoding.UTF8.GetBytes(toEncrypt), key, iv);

      Console.WriteLine($"Encrypted: {string.Join("", encrypted.Select(b => b.ToString("x2")))}");

      var decrypted = MemCrypt.Decrypt(encrypted, key, iv);
      Console.WriteLine($"Decrypted: {Encoding.UTF8.GetString(decrypted)}");

      var encrypted2 = MemCrypt.Encrypt("Yeah!", key, iv);
      Console.WriteLine(encrypted2); // R1/5gYvcxyR2vzPjnT7yaQ==

      var decrypted2 = MemCrypt.Decrypt(encrypted2, key, iv);
      Console.WriteLine(decrypted2); // Yeah!

      Array.Clear(key, 0, key.Length);
      Array.Clear(iv, 0, iv.Length);

      Console.WriteLine($"Key: {string.Join("", key.Select(b => b.ToString("x2")))}");
      Console.WriteLine($"IV : {string.Join("", iv.Select(b => b.ToString("x2")))}");
   }
}