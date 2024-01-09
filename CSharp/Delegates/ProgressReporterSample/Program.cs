namespace ProgressReporterSample;

internal static class Program
{
   private static void Main()
   {
      ProgressReporter p = WriteProgressToConsole;
      p += WriteProgressToFile;
      Util.HardWork(p);
   }

   private static void WriteProgressToConsole(int percentComplete)
   {
      Console.WriteLine(percentComplete);
   }

   private static void WriteProgressToFile(int percentComplete)
   {
      File.WriteAllText("progress.txt", percentComplete.ToString());
   }

   private delegate void ProgressReporter(int percentComplete);

   private class Util
   {
      public static void HardWork(ProgressReporter p)
      {
         for (var i = 0; i < 10; i++)
         {
            p(i * 10); // Invoke delegate
            Thread.Sleep(100); // Simulate hard work
         }
      }
   }
}