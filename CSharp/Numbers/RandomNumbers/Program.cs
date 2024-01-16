/*
 * Случайные числа, безопасные случайные числа
 */

using System;
using System.Security.Cryptography;

namespace HowToCSharp.Ch05.RandomNumbers
{
   internal class Program
   {
      private static void Main()
      {
         Console.WriteLine("using System.Random:");

         var rand = new Random();
         for (var i = 0; i < 5; i++)
         {
            Console.Write("{0} ", rand.Next());
         }

         Console.WriteLine();

         Console.WriteLine("using System.Random/numbers in range (-10, 10)");
         for (var i = 0; i < 5; i++)
         {
            Console.Write("{0} ", rand.Next(-10, 10));
         }

         Console.WriteLine();

         Console.WriteLine("using RNGCryptoServiceProvider:");

         var cryptRand = new RNGCryptoServiceProvider();
         for (var i = 0; i < 5; i++)
         {
            var bytes = new byte[4];
            cryptRand.GetBytes(bytes);
            var number = BitConverter.ToInt32(bytes, 0);
            Console.Write("{0} ", number);
         }

         Console.WriteLine();
         Console.ReadKey();
      }
   }
}