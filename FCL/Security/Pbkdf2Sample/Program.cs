using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

var encrypted = KeyDerivation.Pbkdf2(
   "stRhong%pword",
   "j78Y#p)/saREN!y3@"u8.ToArray(),
   KeyDerivationPrf.HMACSHA512,
   100,
   64);

var decode = Encoding.UTF8.GetString(encrypted);
Console.WriteLine(decode);