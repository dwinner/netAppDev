using System.Runtime.CompilerServices;

internal class Program
{
   public static void Main()
   {
      for (var i = 0; i < 1000; i++)
      {
         Foo();
      }
   }

   // the following attribute is to disable automatic initialization of local variables
   [SkipLocalsInit]
   private static unsafe void Foo()
   {
      int local;
      var ptr = &local;

      Console.Write(*ptr + " ");

      var a = stackalloc int[10];
      for (var i = 0; i < 10; ++i)
      {
         Console.Write(a[i] + " ");
      }

      Console.WriteLine();
   }
}