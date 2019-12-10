using System;

namespace ConstData
{
   internal class MyMathClass
   {
      public static readonly double Pi;

      static MyMathClass() => Pi = 3.14;
   }

   internal static class Program
   {
      private static void Main()
      {
         Console.WriteLine("***** Fun with Const *****\n");
         Console.WriteLine("The value of PI is: {0}", MyMathClass.Pi);

         // Error! Can't change a constant!
         // MyMathClass.PI = 3.1444;

         Console.ReadLine();
      }

      // This is just for a test.
/*
      private static void LocalConstStringVariable()
      {
         // A local constant data point can be directly accessed.
         const string fixedStr = "Fixed string Data";
         Console.WriteLine(fixedStr);

         // Error!
         // fixedStr = "This will not work!";
      }
*/
   }
}