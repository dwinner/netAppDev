/**
 * Перечисление свойств кодировок
 */

using System;
using System.Text;

namespace EnumEncodings
{
   internal static class Program
   {
      private static void Main()
      {
         Array.ForEach(Encoding.GetEncodings(), info => Console.WriteLine(info.GetEncoding()));
      }
   }
}