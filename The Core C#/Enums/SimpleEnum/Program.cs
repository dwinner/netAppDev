/**
 * Использование перечислений в приложениях.
 */

using System;

#pragma warning disable 219

namespace SumpleEnum
{
   internal static class Program
   {
      private static void Main()
      {
         var binding = BookBinding.Hardcover;

         // NOTE: Преобразование перечисления в целое (и обратно)
         var book = BookLanguage.English;
         const int value = (int) BookLanguage.English;
         Console.WriteLine("Integer value of BookLanguage.English is {0}", value);
         // NOTE: Проверка допустимости значений перечисления при обратном преобразовании
         const BookBinding badBinding = (BookBinding) 9999;
         var isDefined = Enum.IsDefined(typeof (BookBinding), badBinding);
         Console.WriteLine("isDefined: {0}", isDefined);

         Console.Write("Press any key to continue...");
         Console.ReadKey(true);
      }
   }

   internal enum BookBinding // NOTE: Объявление перечислений
   {
      None,
      Hardcover,
      Paperback
   }

   internal enum BookLanguage // NOTE: Объявление перечислений с явным указанием значений
   {
      None = 0,
      English = 1,
      Spanish = 2,
      Italian = 3,
      French = 4,
      Japanese = 5
   }
}