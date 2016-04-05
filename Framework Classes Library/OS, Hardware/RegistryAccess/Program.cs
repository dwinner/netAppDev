/**
 * Обращение к реестру
 */

using System;
using Microsoft.Win32;

namespace RegistryAccess
{
   class Program
   {
      [System.Security.Permissions.RegistryPermission(System.Security.Permissions.SecurityAction.Demand, ViewAndModify = "HKEY_CURRENT_USER")]
      static void Main()
      {
         // Читать из HKLM
         using (RegistryKey hklm = Registry.LocalMachine)
         using (RegistryKey keyRun = hklm.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run"))
         {
            if (keyRun != null)
            {
               foreach (var valueName in keyRun.GetValueNames())
               {
                  Console.WriteLine("Name: {0}\tValue: {1}", valueName, keyRun.GetValue(valueName));
               }
            }
         }

         Console.WriteLine();

         // Создание собственного ключа Реестра для приложения:
         // true означает наше желание иметь возможность записывать в подключ
         using (RegistryKey software = Registry.CurrentUser.OpenSubKey(@"Software", true))
         {
            if (software != null)
            {
               // Volatile означает необходимость удаления этого ключа
               using (RegistryKey myKeyRoot =
                  software.CreateSubKey("CSharp4HowTo",
                     RegistryKeyPermissionCheck.ReadWriteSubTree,
                     RegistryOptions.Volatile))
               {
                  // Автоматическое определение типа
                  if (myKeyRoot != null)
                  {
                     myKeyRoot.SetValue("NumberOfChapters", 28);

                     // Указание типа
                     myKeyRoot.SetValue("Awesomeness", Int64.MaxValue, RegistryValueKind.QWord);

                     // Вывод на экран только что созданной записи
                     foreach (var valueName in myKeyRoot.GetValueNames())
                     {
                        Console.WriteLine("{0}, {1}, {2}", valueName, myKeyRoot.GetValueKind(valueName),
                           myKeyRoot.GetValue(valueName));
                     }
                  }

                  // Удаление записи из Реестра
                  software.DeleteSubKeyTree("CSharp4HowTo");
               }
            }
         }

         Console.WriteLine();
         Console.WriteLine("Press any key to quit...");
         Console.ReadKey();
      }
   }
}
