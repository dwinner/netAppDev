using System.Reflection;

namespace ObtainingType
{
   internal static class Program
   {
      private static void Main()
      {
         var t1 = DateTime.Now.GetType(); // Type obtained at runtime
         var t2 = typeof(DateTime); // Type obtained at compile time
         var t3 = typeof(DateTime[]); // 1-d Array type
         var t4 = typeof(DateTime[,]); // 2-d Array type
         var t5 = typeof(Dictionary<int, int>); // Closed generic type
         var t6 = typeof(Dictionary<,>); // Unbound generic type

         Console.WriteLine(t1);
         Console.WriteLine(t2);
         Console.WriteLine(t3);
         Console.WriteLine(t4);
         Console.WriteLine(t5);
         Console.WriteLine(t6);

         var t = Assembly.GetExecutingAssembly().GetType("Foo.Bar")!;
         Console.WriteLine(t);
      }
   }
}

namespace Foo
{
   public class Bar
   {
      public int Baz;
   }
}