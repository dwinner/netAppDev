using MyExtensionsLibrary;
using System;

// Import our custom namespace.

namespace MyExtnesionsLibraryClient
{
   class Program
   {
      static void Main(string[] args)
      {
         Console.WriteLine("***** Using Library with Extensions *****\n");

         int myInt = 987;
         myInt.DisplayDefiningAssembly();
         Console.WriteLine("{0} is reversed to {1}", myInt, myInt.ReverseDigits());
         Console.ReadLine();
      }
   }
}

