/**
 * Создание собственной схемы кодирования
 */

using System;
using System.Text;

namespace Rot13StringEncoder
{
   class Program
   {
      static void Main(string[] args)
      {
         Rot13Encoder encoder = new Rot13Encoder();

         const string original = "hellocsharp";

         byte[] bytes = encoder.GetBytes(original);

         // Как это будет выглядеть, если мы предположим,
         // что применялась кодировка ASCII
         Console.WriteLine("Original: {0}", original);
         Console.WriteLine("ASCII interpretation: {0}", Encoding.ASCII.GetString(bytes));
         Console.WriteLine("Rot13 interpretation: {0}", encoder.GetString(bytes));

         Console.ReadKey();
      }
   }
}
