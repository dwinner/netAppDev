using System;
using CarLibrary;

namespace CodeBaseClient
{
   class Program
   {
      static void Main()
      {
         Console.WriteLine("***** Fun with CodeBases *****");
         var c = new SportsCar();
         Console.WriteLine("Sports car has been allocated.");
         Console.ReadLine();
      }
   }
}
