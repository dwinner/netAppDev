/**
 * Способы преобразования в символы
 */

using System;

namespace ConversionVariants
{
   internal static class Program
   {
      private static void Main()
      {
         // Преобразование "число - символ" посредством приведения типов C#
         var c = (char)65;
         Console.WriteLine(c); // A

         int n = c;
         Console.WriteLine(n); // 65
         c = unchecked((char)(0x10000 + 65));
         Console.WriteLine(c); // A

         // Преобразование "число - символ" с помощью типа Convert
         c = Convert.ToChar(65);
         Console.WriteLine(c); // A
         n = Convert.ToInt32(c);
         Console.WriteLine(n); // 65

         // Этот код демонстрирует проверку допустимых значений для Convert
         try
         {
            c = Convert.ToChar(70000); // Слишком много для 16-ти разрядов
            Console.WriteLine(c);
         }
         catch (OverflowException)
         {
            Console.WriteLine("Can't convert 70000 to a char");
         }

         // Преобразование "число-символ" с помощью интерфейса IConvertible
         c = ((IConvertible)65).ToChar(null);
         Console.WriteLine(c); // A
         n = ((IConvertible)c).ToInt32(null);
         Console.WriteLine(n); // 65
      }
   }
}