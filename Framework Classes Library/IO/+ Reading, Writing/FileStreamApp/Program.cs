using System;
using System.IO;
using System.Text;

namespace FileStreamApp
{
   class Program
   {
      static void Main()
      {
         Console.WriteLine("***** Fun with FileStreams *****\n");

         try
         {
            // Получаем объект FileStream.
            using (Stream fStream = File.Open(@"C:\myMessage.dat", FileMode.Create))
            {
               // Кодируем строку как массив байтов в кодировке по умолчанию.
               string msg = "Hello!";
               byte[] msgAsByteArray = Encoding.Default.GetBytes(msg);

               // Записываем байты в файл
               fStream.Write(msgAsByteArray, 0, msgAsByteArray.Length);

               // Сбросим внутренюю позицию потока
               fStream.Position = 0;

               // Читаем из файла "сырые" байты
               Console.Write("Your message as an array of bytes: ");
               byte[] bytesFromFile = new byte[msgAsByteArray.Length];
               for (int i = 0; i < msgAsByteArray.Length; i++)
               {
                  bytesFromFile[i] = (byte)fStream.ReadByte();
                  Console.Write(bytesFromFile[i]);
               }

               // Отображаем декодированное сообщение
               Console.Write("\nDecoded Message: ");
               Console.WriteLine(Encoding.Default.GetString(bytesFromFile));
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
