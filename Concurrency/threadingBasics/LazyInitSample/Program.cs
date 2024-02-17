namespace LazyInitSample;

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
   private Expensive? _expensive;

   public Expensive? Expensive
   { // Implement double-checked locking
      get
      {
         LazyInitializer.EnsureInitialized(ref _expensive, () => new Expensive());
         return _expensive;
      }
   }
}

internal class Expensive
{ /* Suppose this is expensive to construct */
}