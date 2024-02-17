namespace AsyncLocal_Concurrent;

internal static class Program
{
   private static readonly AsyncLocal<string> AsyncLocalTest = new();

   private static void Main()
   {
      new Thread(() => Test("one")).Start();
      new Thread(() => Test("two")).Start();

      Console.ReadLine();
   }

   private static async void Test(string value)
   {
      AsyncLocalTest.Value = value;
      await Task.Delay(1000).ConfigureAwait(false);
      Console.WriteLine("{0} {1}", value, AsyncLocalTest.Value);
   }
}