// Create public/private keypair, and save to disk:

using System.Security.Cryptography;
using System.Text;

using (var rsa = new RSACryptoServiceProvider())
{
   File.WriteAllText("PublicKeyOnly.xml", rsa.ToXmlString(false));
   File.WriteAllText("PublicPrivate.xml", rsa.ToXmlString(true));
}

// Encrypt. Note that we can directly encrypt only small messages.

var data = "Message to encrypt"u8.ToArray();

var publicKeyOnly = File.ReadAllText("PublicKeyOnly.xml");
var publicPrivate = File.ReadAllText("PublicPrivate.xml");

byte[] encrypted;

using (var rsaPublicOnly = new RSACryptoServiceProvider())
{
   rsaPublicOnly.FromXmlString(publicKeyOnly);
   encrypted = rsaPublicOnly.Encrypt(data, true);

   // The next line would throw an exception because you need the private
   // key in order to decrypt:
   // decrypted = rsaPublicOnly.Decrypt (encrypted, true);
}

// Decrypt:

using (var rsaPublicPrivate = new RSACryptoServiceProvider())
{
   // With the private key we can successfully decrypt:
   rsaPublicPrivate.FromXmlString(publicPrivate);
   var decrypted = rsaPublicPrivate.Decrypt(encrypted, true);
   var decryptedStr = Encoding.UTF8.GetString(decrypted);
   Console.WriteLine(decryptedStr);
}