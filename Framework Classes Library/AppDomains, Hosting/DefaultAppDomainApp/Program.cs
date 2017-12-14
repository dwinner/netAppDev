using System;
using System.Linq;

namespace DefaultAppDomainApp
{
   // Домены приложений.
   internal static class Program
   {
      private static void Main()
      {
         Console.WriteLine("***** Fun with the default app domain *****\n");
         SetupAssemblyLoadedHandler();
         DisplayDadStats();
         ListAllAssembliesInAppDomain();

         Console.ReadKey(true);
      }

      /// <summary>
      ///    Информация о домене по умолчанию.
      /// </summary>
      private static void DisplayDadStats()
      {
         // Получение доступа к домену приложения, используемому для
         // текущего потока по умолчанию.
         var defaultAd = AppDomain.CurrentDomain;

         // Вывод в окно консоли статистических данных об этом домене.
         Console.WriteLine("Name of this domain: {0}",
            defaultAd.FriendlyName); // Читабельное имя
         Console.WriteLine("ID of domain in this process: {0}",
            defaultAd.Id); // Идентификатор
         Console.WriteLine("Is this the default domain?: {0}",
            defaultAd.IsDefaultAppDomain()); // Используется ли по умолчанию
         Console.WriteLine("Base directory of this domain: {0}",
            defaultAd.BaseDirectory); // Базовый каталог
      }

      /// <summary>
      ///    Перечисление загружаемых сборок.
      /// </summary>
      private static void ListAllAssembliesInAppDomain()
      {
         // Доступ к домену приложения по умолчанию для текущего потока.
         var defaultAd = AppDomain.CurrentDomain;

         // Извлечение списка всех сборок, загруженных в этот домен приложения.
         var loadedAssemblies = from assembly in defaultAd.GetAssemblies()
            orderby assembly.GetName().Name
            select assembly;
         // Assembly[] loadedAssemblies = defaultAd.GetAssemblies();
         Console.WriteLine("***** Here are the assemblies loaded in {0} *****\n",
            defaultAd.FriendlyName);
         foreach (var loadedAssembly in loadedAssemblies)
         {
            Console.WriteLine("-> Name: {0}", loadedAssembly.GetName().Name);
            Console.WriteLine("-> Version: {0}", loadedAssembly.GetName().Version);
         }
      }

      /// <summary>
      ///    Получение уведомлений о загрузке сборок в домене по умолчанию.
      /// </summary>
      private static void SetupAssemblyLoadedHandler()
      {
         var defaultDomain = AppDomain.CurrentDomain;
         defaultDomain.AssemblyLoad += (sender, args) => Console.WriteLine("{0} has been loaded!",
            args.LoadedAssembly.GetName().Name);
      }
   }
}