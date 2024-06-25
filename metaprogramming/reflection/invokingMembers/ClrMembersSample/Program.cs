using System.Diagnostics;

namespace ClrMembersSample;

internal static class Program
{
   private static void Main()
   {
      foreach (var mi in typeof(Test).GetMethods())
      {
         Console.Write(mi.Name + "  ");
      }

      var pi = typeof(Console).GetProperty("Title");
      Console.WriteLine(pi);

      var getter = pi.GetGetMethod();
      Console.WriteLine(getter);

      var setter = pi.GetSetMethod();
      Console.WriteLine(setter);

      var len = pi.GetAccessors().Length;
      Console.WriteLine(len);

      var p = getter.DeclaringType.GetProperties()
         .First(x => x.GetAccessors(true).Contains(getter));

      Debug.Assert(p == pi);
   }
}

internal class Test
{
   public int X
   {
      get => 0;
      set { }
   }
}

internal class Walnut
{
   private bool cracked;

   public void Crack()
   {
      cracked = true;
   }
}