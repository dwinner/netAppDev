using System.Diagnostics;

namespace PerfDelegates;

internal static class Program
{
   private static void Main()
   {
      var trimMethod = typeof(string).GetMethod("Trim", Type.EmptyTypes);

      // First let's test the performance when calling Invoke
      var sw = Stopwatch.StartNew();

      for (var i = 0; i < 1_000_000; i++)
      {
         trimMethod.Invoke("test", null);
      }

      sw.Stop();

      // Now let's test the performance when using a delegate:
      var trim = (StringToString)Delegate.CreateDelegate(typeof(StringToString), trimMethod);
      sw.Restart();

      for (var i = 0; i < 1_000_000; i++)
      {
         trim("test");
      }

      sw.Stop();
   }

   private delegate string StringToString(string s);
}