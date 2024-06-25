using System.Reflection;

namespace ReflectingNullability;

internal class Program
{
   public string? Foo { get; set; }
   public string Bar { get; set; } = "Bar";

   public string?[]? FooArray { get; set; }
   public string[] BarArray { get; set; } = { "bar" };

   private static void Main()
   {
      var p = new Program();
      p.PrintPropertyNullability(p.GetType().GetProperty("Foo")!);
      p.PrintPropertyNullability(p.GetType().GetProperty("Bar")!);
      p.PrintPropertyNullability(p.GetType().GetProperty("FooArray")!);
      p.PrintPropertyNullability(p.GetType().GetProperty("BarArray")!);
   }

   private void PrintPropertyNullability(PropertyInfo pi)
   {
      var info = new NullabilityInfoContext().Create(pi);
      Console.WriteLine($"{pi.Name}: {info}");
   }
}