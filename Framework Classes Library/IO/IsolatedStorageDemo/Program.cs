/**
 * Хранение данных в изолированном хранилище
 */

using System;
using System.Globalization;
using System.IO;
using System.IO.IsolatedStorage;

namespace IsolatedStorageDemo
{
   class Program
   {
      static void Main(string[] args)
      {
         Console.WriteLine("Run with command line arg -r to remove isolated storage for this app/user");

         // Получить изолированную память для этого домена приложения и этого пользователя
         using (IsolatedStorageFile file = IsolatedStorageFile.GetUserStoreForDomain())
         {
            // Установить каталог
            if (!file.DirectoryExists("Dummy"))
            {
               file.CreateDirectory("Dummy");
            }
            Console.WriteLine("Accesses:");

            // Выполнить чтение и запись файла в каталоге
            using (IsolatedStorageFileStream stream = file.OpenFile(@"Dummy\accesses.txt", FileMode.OpenOrCreate))
            using (TextReader reader = new StreamReader(stream))
            using (TextWriter writer = new StreamWriter(stream))
            {
               string line;
               do
               {
                  line = reader.ReadLine();
                  if (line != null)
                  {
                     Console.WriteLine(line);
                  }
               }
               while (line != null);

               writer.WriteLine(DateTime.Now.ToString(CultureInfo.InvariantCulture));
            }
            if (args.Length > 0 && args[0] == "-r")
            {
               Console.WriteLine("Removing isolated storage for this user/app-domain");
               file.Remove();
            }
         }

         Console.ReadKey();
      }
   }
}
