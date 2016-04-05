using System;
using System.IO;

namespace SimpleFileIO
{
   class Program
   {
      static void Main(string[] args)
      {
         Console.WriteLine("***** Simple IO with the File Type *****\n");
         try
         {
            string[] myTasks =
                {
                     "Fix bathroom sink", "Call Dave",
                     "Call Mom and Dad", "Play Xbox 360"
                };

            // Запишем все данные на файл.
            File.WriteAllLines(@"C:\tasks.txt", myTasks);

            // Прочитаем их обратно
            foreach (string task in File.ReadAllLines(@"C:\tasks.txt"))
            {
               Console.WriteLine("TODO: {0}", task);
            }
         }
         catch (Exception ex)
         {
            Console.WriteLine(ex.Message);
         }
         Console.ReadLine();
      }
   }

}
