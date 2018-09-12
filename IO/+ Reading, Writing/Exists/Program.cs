/**
 * Проверка существования файла или каталога
 */

using System;
using System.IO;

namespace Exists
{
   class Program
   {
      static void Main(string[] args)
      {
         if (args.Length < 1)
         {
            Console.WriteLine("Usage: Exists filename");
            return;
         }
         string target = args[0];
         if (File.Exists(target))
         {
            Console.WriteLine("File {0} exists", target);
         }
         else if (Directory.Exists(target))
         {
            Console.WriteLine("Directory {0} exists", target);
         }
         else
         {
            Console.WriteLine("{0} does not exist", target);
         }
      }
   }
}
