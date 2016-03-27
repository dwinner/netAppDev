// Воскрешение объектов

using System;

namespace _09_Resurrection
{
   internal static class Program
   {
      internal static object ResurrectionObj;

      private static void Main()
      {
      }
   }

   internal sealed class SomeType
   {
      ~SomeType()
      {
         Program.ResurrectionObj = this;
         GC.ReRegisterForFinalize(this);
      }
   }
}