/**
 * Краткий пример расширяющего метода
 */

using System;

namespace _01_QuickExample
{
   public static class ExtensionMethods
   {
      public static void SendToLog(this string aString)
      {
         Console.WriteLine(aString);
      }
   }

   class Program
   {
      static void Main()
      {
         const string str = "Полезная для протоколирования информация";

         // Вызов расширяющего метода
         str.SendToLog();

         // Вызов того же метода старым способом
         ExtensionMethods.SendToLog(str);

         Console.ReadKey();
      }
   }
}
