using System.Reflection;

namespace ReflectingAssemblies
{
   internal static class Program
   {
      private static void Main()
      {
         var currentAssem = Assembly.GetExecutingAssembly();
         var t = currentAssem.GetType("Demos.TestProgram");
         Console.WriteLine(t.Name);

         var allTypes = currentAssem.GetTypes();
         foreach (var type in allTypes)
         {
            Console.WriteLine(type.FullName);
         }
      }
   }
}

namespace Demos
{
   internal class TestProgram
   {
   }

   internal class SomeOtherType
   {
   }
}