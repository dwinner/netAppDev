/*
 * Отражение разделяемых сборок.
 * Пользователь: Denis
 * Дата: 22.01.2013
 * Время: 15:14
 */
using System;
using System.Linq;
using System.Reflection;

namespace SharedAsmReflector
{
   class Program
   {
      public static void Main(string[] args)
      {
         Console.WriteLine("***** The shared assembly reflector app *****\n");
         
         // Выполнение загрузки System.Windows.Forms.dll из GAC.
         string displayName = @"System.Windows.Forms,Version=4.0.0.0,PublicKeyToken=B77A5C561934E089,Culture=""";         
         Assembly asm = Assembly.Load(displayName);
         DisplayInfo(asm);
         Console.WriteLine("Done!");         
         
         Console.Write("Press any key to continue . . . ");
         Console.ReadKey(true);
      }
      
      private static void DisplayInfo(Assembly asm)
      {
         Console.WriteLine("***** Info about Assembly *****");
         
         Console.WriteLine("Loaded from GAC? {0}", asm.GlobalAssemblyCache);
         Console.WriteLine("Asm name: {0}", asm.GetName().Name);
         Console.WriteLine("Asm version: {0}", asm.GetName().Version);
         Console.WriteLine("Asm culture: {0}", asm.GetName().CultureInfo.DisplayName);
         Console.WriteLine("\nHere are the public enums:");
         
         // Использование Linq-запроса для нахождения общедоступных перечислений.
         Type[] types = asm.GetTypes();
         var publicEnums = from pe in types where pe.IsEnum && pe.IsPublic select pe;
         foreach (var pe in publicEnums)
         {
            Console.WriteLine(pe);
         }
      }
   }
}