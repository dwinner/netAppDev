namespace FuncDisposable;

internal static class Program
{
   private static void Main()
   {
      var foo = new Foo();

      // Test it without calling SuspendEvents()
      foo.FireSomeEvent();

      // Now test it with event suspension:
      using (foo.SuspendEvents())
      {
         foo.FireSomeEvent();
      }

      // Now test it again without calling SuspendEvents()
      foo.FireSomeEvent();
   }
}

// Reusable class