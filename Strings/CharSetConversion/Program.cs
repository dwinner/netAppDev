/**
 * Преобразования между кодировками
 */

using System;
using System.Text;

namespace _06_CharSetConversion
{
   class Program
   {
      static void Main()
      {
         const string leUnicodeStr = "здорово!";
         Encoding leUnicode = Encoding.Unicode;
         Encoding beUnicode = Encoding.BigEndianUnicode;
         Encoding utf8 = Encoding.UTF8;
         byte[] leUnicodeBytes = leUnicode.GetBytes(leUnicodeStr);
         byte[] beUnicodeBytes = Encoding.Convert(leUnicode, beUnicode, leUnicodeBytes);
         byte[] utf8Bytes = Encoding.Convert(leUnicode, utf8, leUnicodeBytes);
         Console.WriteLine("Исходная строка: {0}\n", leUnicodeStr);

         Console.WriteLine("Байты Unicode, сначала младший: ");
         var builder = new StringBuilder();
         foreach (byte aByte in leUnicodeBytes)
         {
            builder.Append(aByte).Append(" : ");
         }
         Console.WriteLine("{0}\n", builder);

         Console.WriteLine("Байты Unicode, сначала старший:");
         builder.Clear();
         foreach (byte beUnicodeByte in beUnicodeBytes)
         {
            builder.Append(beUnicodeByte).Append(" : ");
         }
         Console.WriteLine("{0}\n", builder);

         Console.WriteLine("Байты UTF8:");
         builder.Clear();
         foreach (byte utf8Byte in utf8Bytes)
         {
            builder.Append(utf8Byte).Append(" : ");
         }
         Console.WriteLine(builder);

         Console.ReadKey();
      }
   }
}
