using System.Security.Cryptography;

using var rsa = new RSACryptoServiceProvider();
var publicKeyOnly = rsa.ToXmlString(false);
File.WriteAllText("PublicKeyOnly.xml", publicKeyOnly);

var publicPrivate = rsa.ToXmlString(true);
File.WriteAllText("PublicPrivate.xml", publicPrivate);