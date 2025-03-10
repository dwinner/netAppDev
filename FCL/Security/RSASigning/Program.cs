﻿using System.Security.Cryptography;

var data = "Message to sign"u8.ToArray();
byte[] publicKey;
byte[] signature;
object hasher = SHA1.Create(); // Our chosen hashing algorithm.

// Generate a new key pair, then sign the data with it:
using (var publicPrivate = new RSACryptoServiceProvider())
{
   signature = publicPrivate.SignData(data, hasher);
   publicKey = publicPrivate.ExportCspBlob(false); // get public key
}

// Create a fresh RSA using just the public key, then test the signature.
using (var publicOnly = new RSACryptoServiceProvider())
{
   publicOnly.ImportCspBlob(publicKey);
   Console.WriteLine(publicOnly.VerifyData(data, hasher, signature)); // True

   // Let's now tamper with the data, and recheck the signature:
   data[0] = 0;
   Console.WriteLine(publicOnly.VerifyData(data, hasher, signature)); // False

   // The following throws an exception as we're lacking a private key:
   /*signature =*/
   publicOnly.SignData(data, hasher);
}