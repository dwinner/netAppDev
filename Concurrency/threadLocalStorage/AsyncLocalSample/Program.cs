namespace AsyncLocalSample;

internal static class Program
{
   private static readonly AsyncLocal<string> AsyncLocalTest = new();

   private static async Task Main()
   {
      Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
      AsyncLocalTest.Value = "test";

      await Task.Delay(1000).ConfigureAwait(false);

      Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
      Console.WriteLine(AsyncLocalTest.Value);
   }
}