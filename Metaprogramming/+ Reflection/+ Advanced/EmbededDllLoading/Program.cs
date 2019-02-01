// Загрузка внедренных в исполняемую сборку dll-файлов

using System.Diagnostics;
using System.Reflection;
using Person.Lib;
using static System.AppDomain;
using static System.Console;

namespace EmbededDllLoading
{
   internal static class Program
   {
      static Program()
      {
         CurrentDomain.AssemblyResolve += (sender, args) =>
         {
            var resourceName = $"AssemblyLoadingAndReflection.{new AssemblyName(args.Name)}.dll";
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
            {
               Debug.Assert(stream != null);
               var assemblyData = new byte[stream.Length];
               stream.Read(assemblyData, 0, assemblyData.Length);
               return Assembly.Load(assemblyData);
            }
         };
      }

      private static void Main()
      {
         var humanPerson = new HumanPerson();
         WriteLine("Person: {0}", humanPerson);
      }
   }
}