namespace LazyOfT;

internal static class Program
{
   private static void Main()
   {
      var expensive = new Foo().Expensive;
      Console.WriteLine(expensive);
   }
}

internal class Foo
{
   private readonly Lazy<Expensive> _expensive = new(() => new Expensive(), true);
   public Expensive Expensive => _expensive.Value;
}

internal class Expensive
{ /* Suppose this is expensive to construct */
}