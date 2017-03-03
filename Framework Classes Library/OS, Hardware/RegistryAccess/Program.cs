/**
 * Обращение к реестру
 */

using System;
using System.Security.Permissions;
using Microsoft.Win32;

namespace RegistryAccess
{
   internal static class Program
   {
      [RegistryPermission(SecurityAction.Demand, ViewAndModify = "HKEY_CURRENT_USER")]
      private static void Main()
      {
         // Читать из HKLM
         using (var hklm = Registry.LocalMachine)
         using (var keyRun = hklm.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run"))
         {
            if (keyRun != null)
               foreach (var valueName in keyRun.GetValueNames())
                  Console.WriteLine("Name: {0}\tValue: {1}", valueName, keyRun.GetValue(valueName));
         }

         Console.WriteLine();

         // Создание собственного ключа Реестра для приложения:
         // true означает наше желание иметь возможность записывать в подключ
         using (var software = Registry.CurrentUser.OpenSubKey(@"Software", true))
         {
            if (software != null)
               using (var myKeyRoot =
                  software.CreateSubKey("CSharp4HowTo",
                     RegistryKeyPermissionCheck.ReadWriteSubTree,
                     RegistryOptions.Volatile))
               {
                  // Автоматическое определение типа
                  if (myKeyRoot != null)
                  {
                     myKeyRoot.SetValue("NumberOfChapters", 28);

                     // Указание типа
                     myKeyRoot.SetValue("Awesomeness", long.MaxValue, RegistryValueKind.QWord);

                     // Вывод на экран только что созданной записи
                     foreach (var valueName in myKeyRoot.GetValueNames())
                        Console.WriteLine("{0}, {1}, {2}", valueName, myKeyRoot.GetValueKind(valueName),
                           myKeyRoot.GetValue(valueName));
                  }

                  // Удаление записи из Реестра
                  software.DeleteSubKeyTree("CSharp4HowTo");
               }
         }

         Console.WriteLine();
         Console.WriteLine("Press any key to quit...");
         Console.ReadKey();
      }
   }
}