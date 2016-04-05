/**
 * Создание, чтение и запись текстового файла.
 */

using System;
using System.IO;

namespace ConsoleToFile
{
   class Program
   {
      static void Main(string[] args)
      {
         if (args.Length < 2)
         {
            Console.WriteLine("Usage: ConsoleToFile filename output1 output2 output3 ...");
            Console.ReadKey();
            return;
         }
         // Записать в файл каждый аргумент из командной строки
         string destFilename = args[0];
         using (StreamWriter writer = File.CreateText(destFilename))
         {
            for (int i = 1; i < args.Length; i++)
            {
               writer.WriteLine(args[i]);
            }
         }
         Console.WriteLine("Wrote args to file {0}", destFilename);

         // Просто прочитать файл и вывести его на консоль
         using (StreamReader reader = File.OpenText(destFilename))
         {
            string line;
            do
            {
               line = reader.ReadLine();
               Console.WriteLine(line);
            }
            while (line != null);
         }
      }
   }
}
