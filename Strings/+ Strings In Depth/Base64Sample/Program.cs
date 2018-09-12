/**
 * Кодирование в base64
 */

using System;

namespace Base64Sample
{
   internal static class Program
   {
      private static void Main()
      {
         // Получаем набор из 10 байт, сгенерированных случайным образом
         var rndBytes = new byte[10];
         var rnd = new Random();
         rnd.NextBytes(rndBytes);

         // Отображаем байты
         Console.WriteLine(BitConverter.ToString(rndBytes));

         // Кодируем байты в строку в кодировке base-64 и выводим эту строку
         var base64String = Convert.ToBase64String(rndBytes);
         Console.WriteLine(base64String);

         // Декодируем строку в кодировке base-64 обратно в байты и выводим ее
         rndBytes = Convert.FromBase64String(base64String);
         Console.WriteLine(BitConverter.ToString(rndBytes));
      }
   }
}