﻿using System;

namespace SimpleFinalize
{
   class Program
   {
      static void Main(string[] args)
      {
         Console.WriteLine("***** Fun with Finalizers *****\n");
         Console.WriteLine("Hit the return key to shut down this app");
         Console.WriteLine("and force the GC to invoke Finalize()");
         Console.WriteLine("for finalizable objects created in this AppDomain.");
         Console.ReadLine();
         MyResourceWrapper rw = new MyResourceWrapper();
      }
   }
}
