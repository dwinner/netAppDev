using System.Security.Cryptography;

byte[] key = { 145, 12, 32, 245, 98, 132, 98, 214, 6, 77, 131, 44, 221, 3, 9, 50 };
byte[] iv = { 15, 122, 132, 5, 93, 198, 44, 31, 9, 39, 241, 49, 250, 188, 80, 7 };

byte[] data = { 1, 2, 3, 4, 5 }; // This is what we're encrypting.

using (SymmetricAlgorithm algorithm = Aes.Create())
using (var encryptor = algorithm.CreateEncryptor(key, iv))
using (Stream fileStream = File.Create("encrypted.bin"))
using (Stream cryptoStream = new CryptoStream(fileStream, encryptor, CryptoStreamMode.Write))
{
   cryptoStream.Write(data, 0, data.Length);
}

using (SymmetricAlgorithm algorithm = Aes.Create())
using (var decryptor = algorithm.CreateDecryptor(key, iv))
using (Stream fileStream = File.OpenRead("encrypted.bin"))
using (Stream cryptoStream = new CryptoStream(fileStream, decryptor, CryptoStreamMode.Read))
{
   for (int b; (b = cryptoStream.ReadByte()) > -1;)
   {
      Console.Write(b + " "); // 1 2 3 4 5
   }
}