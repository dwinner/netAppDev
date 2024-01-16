/*
 * Обработка очень больших чисел
 */

using System;
using System.Numerics;
using System.Security.Cryptography;

namespace BigIntDemo
{
   internal static class Program
   {
      private static void Main()
      {
         BigInteger a = ulong.MaxValue;
         BigInteger b = ulong.MaxValue;

         Console.WriteLine("Really, Really Big: {0:R} x {1:R} = {2:R}", a, b, a * b);
         Console.WriteLine("Painfully huge:  Pow({0:R}, {1}) = {2:R}", a, 100, BigInteger.Pow(a, 100));

         const string numberToParse = "234743652378423045783479556793498547534684795672309482359874390";
         var bigInteger = BigInteger.Parse(numberToParse);
         Console.WriteLine("N0: {0:N0}", bigInteger);
         Console.WriteLine("R: {0:R}", bigInteger);
         Console.ReadKey();
      }

      private static void OneMoreSample()
      {
         // BigInteger supports arbitrary precision.

         BigInteger twentyFive = 25; // implicit cast from integer

         var googol = BigInteger.Pow(10, 100);

         // Alternatively, you can Parse a string: 
         var googolFromString = BigInteger.Parse("1".PadRight(101, '0'));

         Console.WriteLine(googol.ToString());

         var g1 = 1e100; // implicit cast
         var g2 = (BigInteger)g1; // explicit cast
         Console.WriteLine($"Note loss of precision: {g2}");

         // This uses the System.Security.Cryptography namespace:
         var rand = RandomNumberGenerator.Create();
         var bytes = new byte[32];
         rand.GetBytes(bytes);
         var bigRandomNumber = new BigInteger(bytes); // Convert to BigInteger
         Console.WriteLine($"Big random number: {bigRandomNumber}");
      }
   }
}