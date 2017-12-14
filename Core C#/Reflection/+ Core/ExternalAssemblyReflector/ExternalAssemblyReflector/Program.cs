using System;
using System.Reflection;

namespace ExternalAssemblyReflector
{
   internal static class Program
   {
      private static void Main()
      {
         Console.WriteLine("***** External Assembly Viewer *****");

         do
         {
            Console.WriteLine("\nEnter an assembly to evaluate");
            Console.WriteLine("Or enter Q to quit");
            var asmName = Console.ReadLine();
            if (asmName.ToUpper() == "Q")
               break;
            try
            {
               var asm = Assembly.Load(asmName);
               DisplayTypesInAsm(asm);
            }
            catch
            {
               Console.WriteLine("Sorry, can't find assembly.");
            }
         } while (true);

         Console.ReadKey(true);
      }

      private static void DisplayTypesInAsm(Assembly asm)
      {
         Console.WriteLine("\n***** Types in Assembly *****");
         Console.WriteLine("->{0}", asm.FullName);
         var types = asm.GetTypes();
         foreach (var t in types)
            Console.WriteLine("Type: {0}", t);
         Console.WriteLine();
      }
   }
}