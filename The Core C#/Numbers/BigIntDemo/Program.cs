/**
 * Обработка очень больших чисел
 */

using System;
using System.Numerics;

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
   }
}