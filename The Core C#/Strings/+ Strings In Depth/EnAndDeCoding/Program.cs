/**
 * Кодирование и декодирование символов
 */

using System;
using System.Text;

namespace EnAndDeCoding
{
   internal static class Program
   {
      private static void Main()
      {
         // Эту строку мы будем кодировать
         const string s = "Hi there";

         // Получаем объект, производный от Encoding, который "умеет" выполнять кодирование и декодирование с использованием UTF-8
         var utf8 = Encoding.UTF8;

         // Выполняем кодирование строки в массив байтов
         var encodedBytes = utf8.GetBytes(s);

         // Показываем значение закодированных байтов
         Console.WriteLine("Encoded bytes: {0}", BitConverter.ToString(encodedBytes));

         // Выполняем декодирование массива байтов обратно с строку
         var decodedString = utf8.GetString(encodedBytes);

         // Показываем декодированную строку
         Console.WriteLine("Decoded string: {0}", decodedString);
      }
   }
}