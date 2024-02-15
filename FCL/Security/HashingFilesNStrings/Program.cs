using System.Security.Cryptography;

byte[] hash;

// Compute hash from file:

File.WriteAllText("test.txt", """
                              
                              	The quick brown fox jumps over the lazy dog.
                              	The quick brown fox jumps over the lazy dog.
                              	The quick brown fox jumps over the lazy dog.
                              """);

using (Stream fileStream = File.OpenRead("test.txt"))
{
   hash = SHA1.Create().ComputeHash(fileStream); // SHA1 hash is 20 bytes long
}

Array.ForEach(hash, item => Console.Write("{0} ", item));

Console.WriteLine();

// Compute hash from string:
var data = "stRhong%pword"u8.ToArray();
var hash2 = SHA256.HashData(data) /* SHA256.Create().ComputeHash(data) */;

Array.ForEach(hash2, item => Console.Write("{0} ", item));