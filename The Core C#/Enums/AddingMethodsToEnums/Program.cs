/**
 * Добавление методов к перечислимым типам
 */

using System;
using System.IO;

namespace AddingMethodsToEnums
{
   internal static class Program
   {
      private static void Main()
      {
         var fa = FileAttributes.System;
         fa = fa.Set(FileAttributes.ReadOnly);
         fa = fa.Clear(FileAttributes.System);
         fa.ForEach(fileAttributes => Console.WriteLine(fileAttributes));
      }
   }
}