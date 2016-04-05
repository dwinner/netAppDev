/**
 * Ковариантность массивов
 */

using System;
using System.Collections.Generic;

namespace _06_ArrayCovariance
{
   class Program
   {
      static void Main()
      {
         var strings = new[] { "One", "Two", "Three" };
         DisplayStrings(strings);

         // Правила ковариантности массивов допускают следующее присваивание
         object[] objects = strings;

         // Но что теперь произойдет?
         objects[1] = new object(); // Warn: ArrayTypeMismatchException
         DisplayStrings(strings);
      }

      static void DisplayStrings(IEnumerable<string> strings)
      {
         Console.WriteLine("----- Вывод строк -----");
         foreach (var s in strings)
         {
            Console.WriteLine(s);
         }
      }
   }
}
