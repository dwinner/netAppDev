using System.Diagnostics;

namespace PerfDelegates;

internal static class Program
{
   private static void Main()
   {
      var trimMethod = typeof(string).GetMethod("Trim", Type.EmptyTypes);

      // First let's test the performance when calling Invoke
      var stopwatch = Stopwatch.StartNew();

      for (var i = 0; i < 1_000_000; i++)
      {
         trimMethod.Invoke("test", null);
      }

      stopwatch.Stop();

      Console.WriteLine($"Calling invoke: {stopwatch.Elapsed}");

      // Now let's test the performance when using a delegate:
      var trim = (StringToString)Delegate.CreateDelegate(typeof(StringToString), trimMethod);
      stopwatch.Restart();

      for (var i = 0; i < 1_000_000; i++)
      {
         trim("test");
      }

      stopwatch.Stop();

      Console.WriteLine($"Calling cached delegate: {stopwatch.Elapsed}");
   }

   private delegate string StringToString(string str);
}