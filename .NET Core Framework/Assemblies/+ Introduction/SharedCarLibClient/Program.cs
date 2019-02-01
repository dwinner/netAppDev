using System;
using CarLibrary;

namespace SharedCarLibClient
{
   internal class Program
   {
      private static void Main(string[] args)
      {
         Console.WriteLine("***** Shared Assembly Client *****");
         var c = new SportsCar();
         c.TurboBoost();
         Console.ReadLine();
      }
   }
}