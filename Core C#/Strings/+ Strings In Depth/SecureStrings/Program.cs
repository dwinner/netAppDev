/**
 * Защищенные строки
 */

using System;
using System.Runtime.InteropServices;
using System.Security;

namespace SecureStrings
{
   internal static class Program
   {
      private static void Main()
      {
         using (var secureString = new SecureString())
         {
            Console.Write("Please enter password:");
            while (true)
            {
               var consoleKeyInfo = Console.ReadKey(true);
               if (consoleKeyInfo.Key == ConsoleKey.Enter)
               {
                  break;
               }

               secureString.AppendChar(consoleKeyInfo.KeyChar);
               Console.Write("*");
            }

            Console.WriteLine();

            DisplaySecureString(secureString);
         }
      }

      private static unsafe void DisplaySecureString(SecureString secureStr)
      {
         char* pc = null;
         try
         {
            // Расшифровка SecureString в буфер неуправляемой памяти
            pc = (char*)Marshal.SecureStringToCoTaskMemUnicode(secureStr);

            // Доступ к буферу неуправляемой памяти, который хранит расшифрованную версию SecureString
            for (var i = 0; pc[i] != 0; i++)
            {
               Console.WriteLine(pc[i]);
            }
         }
         finally
         {
            if (pc != null)
            {
               Marshal.ZeroFreeCoTaskMemUnicode((IntPtr)pc);
            }
         }
      }
   }
}