using System.Security.Cryptography;
using System.Text;

byte[] data = { 1, 2, 3, 4, 5 }; // This is what we're encrypting.

using var rsa = new RSACryptoServiceProvider();
Console.WriteLine(rsa.KeySize);
var encrypted = rsa.Encrypt(data, true);
var decrypted = rsa.Decrypt(encrypted, true);
var decode = Encoding.UTF8.GetString(decrypted);
Console.WriteLine(decode);