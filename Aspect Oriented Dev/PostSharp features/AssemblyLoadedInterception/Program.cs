using System;

namespace AssemblyLoadedInterception
{
   internal class Program
   {
      private static void Main(string[] args)
      {
         Console.WriteLine("Hello, AOP");
      }

      //[ModuleInitializer(1)]
      public void ModuleInitializer()
      {
         Console.WriteLine("Module is loaded");
      }
   }
}