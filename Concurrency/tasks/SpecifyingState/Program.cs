namespace SpecifyingState;

internal static class Program
{
   private static async Task Main()
   {
      var task = Task.Factory.StartNew(Greet, "Hello");
      await task.ConfigureAwait(ConfigureAwaitOptions.ForceYielding);
   }

   private static void Greet(object? state)
   {
      Console.Write(state);
   } // Hello
}