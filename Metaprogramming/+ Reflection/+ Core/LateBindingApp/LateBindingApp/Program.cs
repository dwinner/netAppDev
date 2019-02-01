using System;
using System.IO;
using System.Reflection;

namespace LateBindingApp
{
   internal static class Program
   {
      private static void Main()
      {
         Console.WriteLine("***** Fun with Late Binding *****");

         // Попытка загрузки локальной копии CarLibrary.
         Assembly assembly;
         try
         {
            assembly = Assembly.Load("CarLibrary");
         }
         catch (FileNotFoundException fnfEx)
         {
            Console.WriteLine(fnfEx.Message);
            return;
         }
         if (assembly != null)
         {
            CreateUsingLateBinding(assembly);
            InvokeMethodWithArgsUsingLateBinding(assembly);
         }
         Console.ReadKey();
      }

      private static void CreateUsingLateBinding(Assembly asm)
      {
         try
         {
            // Получение метаданных типа Minivan
            var miniVan = asm.GetType("CarLibrary.MiniVan");
            // Создание экземпляра Minivan на лету.
            var obj = Activator.CreateInstance(miniVan);
            Console.WriteLine("Created a {0} using late binding!", obj);
            // Получение информации для TurboBoost.
            var mi = miniVan.GetMethod("TurboBoost");
            // Вызов метода (null означает отсутствие параметров).
            mi.Invoke(obj, null);
         }
         catch (Exception ex)
         {
            Console.WriteLine(ex.Message);
         }
      }

      private static void InvokeMethodWithArgsUsingLateBinding(Assembly asm)
      {
         try
         {
            var sport = asm.GetType("CarLibrary.SportsCar");
            var obj = Activator.CreateInstance(sport);
            var mi = sport.GetMethod("TurnOnRadio");
            mi.Invoke(obj, new object[] {true, 2});
         }
         catch (Exception ex)
         {
            Console.WriteLine(ex.Message);
         }
      }
   }
}