using System.Reflection;
using System.Text;

namespace TypeNames;

internal static class Program
{
   private static void Main()
   {
      {
         var t = typeof(StringBuilder);

         Console.WriteLine(t.Namespace); // System.Text
         Console.WriteLine(t.Name); // StringBuilder
         Console.WriteLine(t.FullName); // System.Text.StringBuilder
      }

      // Nested type names
      {
         var t = typeof(Environment.SpecialFolder);

         Console.WriteLine(t.Namespace); // System
         Console.WriteLine(t.Name); // SpecialFolder
         Console.WriteLine(t.FullName); // System.Environment+SpecialFolder
      }

      // Generic type names
      {
         var t = typeof(Dictionary<,>); // Unbound
         Console.WriteLine(t.Name); // Dictionary'2
         Console.WriteLine(t.FullName); // System.Collections.Generic.Dictionary'2
         Console.WriteLine(typeof(Dictionary<int, string>).FullName);
      }

      // Array and pointer type names
      {
         Console.WriteLine(typeof(int[]).Name); // Int32[]
         Console.WriteLine(typeof(int[,]).Name); // Int32[,]
         Console.WriteLine(typeof(int[,]).FullName); // System.Int32[,]
      }

      // Pointer types
      Console.WriteLine(typeof(byte*).Name); // Byte*

      // ref and out parameter type names
      var x = 3;
      RefMethod(ref x);
   }

   private static void RefMethod(ref int p)
   {
      var t = MethodBase.GetCurrentMethod()?.GetParameters()[0].ParameterType;
      Console.WriteLine(t?.Name ?? string.Empty); // Int32&
   }
}