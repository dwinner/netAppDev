namespace ThreadLocalSample;

internal static class Program
{
   private static readonly ThreadLocal<int> X = new(() => 3);

   private static void Main()
   {
      new Thread(() =>
      {
         Thread.Sleep(1000);
         X.Value++;
         Console.WriteLine(X);
      }).Start();
      new Thread(() =>
      {
         Thread.Sleep(2000);
         X.Value++;
         Console.WriteLine(X);
      }).Start();
      new Thread(() =>
      {
         Thread.Sleep(3000);
         X.Value++;
         Console.WriteLine(X);
      }).Start();
   }
}