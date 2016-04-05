// Большие объекты

using System;
using static System.Console;

namespace _14_BigObjects
{
   internal static class Program
   {
      private static void Main()
      {
         object o = new byte[85000];
         WriteLine(GC.GetGeneration(o));
      }
   }
}