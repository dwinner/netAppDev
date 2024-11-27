using System.Diagnostics;

namespace Boxing
{
   internal static class Program
   {
      private static void Main()
      {
         // How many instances of boxing do you count?
         var val = 13;
         object boxedVal = val;

         val = 14;

         string.Format("val: {0}, boxedVal:{1}", val, boxedVal);
         string.Format("Number of processes on machine: {0}", Process.GetProcesses().Length);

         var foo = new Foo { Name = "Bar" };
         INameable nameable = foo;
         UseItem(nameable);

         // Does this cause boxing?
         int result;
         GetIntByRef(out result);
      }

      private static void GetIntByRef(out int result)
      {
         result = 42;
      }

      private static void UseItem(INameable item)
      {
         // just prevent the compiler from optimizing out unused variables
      }

      private interface INameable
      {
         string Name { get; set; }
      }

      private struct Foo : INameable
      {
         public string Name { get; set; }
      }
   }
}