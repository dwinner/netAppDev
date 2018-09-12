/*
 * Создание новых доменов приложений.
 */

using System;
using System.IO;
using System.Linq;

namespace CustomAppDomains
{
   internal static class Program
   {
      private static void Main()
      {
         Console.WriteLine("***** Fun with custom app domains *****\n");

         // Отображение всех сборок, которые были  загружены в домен по умолчанию.
         var defaultAppDomain = AppDomain.CurrentDomain;
         defaultAppDomain.ProcessExit += (sender, eventArgs) =>
            Console.WriteLine("Default application domain is unloaded!");
         ListAppAssembliesInAppDomain(defaultAppDomain);

         // Создание нового домена приложения.
         MakeNewAppDomain();

         Console.ReadKey(true);
      }

      private static void MakeNewAppDomain()
      {
         // Создание нового домена приложения в текущем процессе.
         var newAppDomain = AppDomain.CreateDomain("SecondAppDomain");
         newAppDomain.DomainUnload += (sender, args) =>
            Console.WriteLine("The second app domain has been unloaded!");
         try
         {
            // Загрузка сборки в новый домен
            newAppDomain.Load("CarLibrary");
         }
         catch (FileNotFoundException fnfEx)
         {
            Console.WriteLine(fnfEx.Message);
         }

         ListAppAssembliesInAppDomain(newAppDomain);
         AppDomain.Unload(newAppDomain); // Выгрузка домена из процесса.
      }

      private static void ListAppAssembliesInAppDomain(AppDomain anAppDomain)
      {
         var loadedAssemblies = from assembly in anAppDomain.GetAssemblies()
                                orderby assembly.GetName().Name
                                select assembly;
         Console.WriteLine("***** Here are the assemblies loaded in {0} *****\n",
            anAppDomain.FriendlyName);
         foreach (var loadedAssembly in loadedAssemblies)
         {
            Console.WriteLine("-> Name: {0}", loadedAssembly.GetName().Name);
            Console.WriteLine("-> Version: {0}\n", loadedAssembly.GetName().Version);
         }
      }
   }
}