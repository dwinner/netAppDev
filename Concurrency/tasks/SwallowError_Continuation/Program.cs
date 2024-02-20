namespace SwallowError_Continuation;

internal static class Program
{
   private static void Main()
   {
      Task.Factory.StartNew(() => throw null!).IgnoreExceptions();
      Console.ReadLine();
   }
}

internal static class Extensions
{
   public static void IgnoreExceptions(this Task task)
   {
      // This could be improved by adding code to log the exception
      task.ContinueWith(_ => { },
         TaskContinuationOptions.OnlyOnFaulted);
   }
}