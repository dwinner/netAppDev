using System;
using System.Runtime.CompilerServices;

namespace CSharp9Features
{
   public static class Init
   {
      [ModuleInitializer]
      public static void EntryForThisModule()
      {
         // This code is the 1st for execution for this module
         Console.WriteLine("Init done");
      }
   }
}