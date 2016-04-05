using System;
using System.IO;
using System.Reflection;

namespace LateBindingApp
{
   class Program
   {
      static void Main(string[] args)
      {
         Console.WriteLine("***** Fun with Late Binding *****");

         // Попытка загрузки локальной копии CarLibrary.
         Assembly assembly = null;
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

      static void CreateUsingLateBinding(Assembly asm)
      {
         try
         {
            // Получение метаданных типа Minivan
            Type miniVan = asm.GetType("CarLibrary.MiniVan");
            // Создание экземпляра Minivan на лету.
            object obj = Activator.CreateInstance(miniVan);
            Console.WriteLine("Created a {0} using late binding!", obj);
            // Получение информации для TurboBoost.
            MethodInfo mi = miniVan.GetMethod("TurboBoost");
            // Вызов метода (null означает отсутствие параметров).
            mi.Invoke(obj, null);
         }
         catch (Exception ex)
         {
            Console.WriteLine(ex.Message);
         }
      }

      static void InvokeMethodWithArgsUsingLateBinding(Assembly asm)
      {
         try
         {
            Type sport = asm.GetType("CarLibrary.SportsCar");
            object obj = Activator.CreateInstance(sport);
            MethodInfo mi = sport.GetMethod("TurnOnRadio");
            mi.Invoke(obj, new object[] { true, 2 });
         }
         catch (Exception ex)
         {
            Console.WriteLine(ex.Message);
         }
      }
   }
}
