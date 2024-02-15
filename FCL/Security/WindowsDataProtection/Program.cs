using System.Security.Cryptography;

byte[] original = { 1, 2, 3, 4, 5 };
Array.ForEach(original, item => Console.Write($"{item} "));

Console.WriteLine();

var scope = DataProtectionScope.CurrentUser;
var encrypted = ProtectedData.Protect(original, null, scope);
Array.ForEach(encrypted, item => Console.Write($"{item} "));

Console.WriteLine();

var decrypted = ProtectedData.Unprotect(encrypted, null, scope);
// decrypted is now {1, 2, 3, 4, 5}
Array.ForEach(decrypted, item => Console.Write($"{item} "));