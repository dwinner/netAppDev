/**
 * Получение пути к каталогам My Documents, My Pictures, и т.д.
 */

using System;

namespace WellKnownDirectories
{
   class Program
   {
      static void Main()
      {
         foreach (Environment.SpecialFolder folder in Enum.GetValues(typeof(Environment.SpecialFolder)))
         {
            string path = Environment.GetFolderPath(folder);
            Console.WriteLine("{0}\t==>\t{1}", folder, path);
         }
         Console.ReadKey();
      }
   }
}
