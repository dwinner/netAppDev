using System.IO.Compression;
using System.Security.Cryptography;

var key = new byte[16];
var iv = new byte[16];

var cryptoRng = RandomNumberGenerator.Create();
cryptoRng.GetBytes(key);
cryptoRng.GetBytes(iv);

using var algorithm = Aes.Create();
using (var encryptor = algorithm.CreateEncryptor(key, iv))
await using (Stream f = File.Create("serious.bin"))
await using (Stream c = new CryptoStream(f, encryptor, CryptoStreamMode.Write))
await using (Stream d = new DeflateStream(c, CompressionMode.Compress))
await using (var w = new StreamWriter(d))
{
   await w.WriteLineAsync("Small and secure!").ConfigureAwait(false);
}

using (var decryptor = algorithm.CreateDecryptor(key, iv))
await using (Stream f = File.OpenRead("serious.bin"))
await using (Stream c = new CryptoStream(f, decryptor, CryptoStreamMode.Read))
await using (Stream d = new DeflateStream(c, CompressionMode.Decompress))
using (var r = new StreamReader(d))
{
   Console.WriteLine(await r.ReadLineAsync().ConfigureAwait(false)); // Small and secure!
}