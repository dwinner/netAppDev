namespace ThreadStaticSample;

internal static class Program
{
   [ThreadStatic] private static int _x;

   private static void Main()
   {
      new Thread(() =>
      {
         Thread.Sleep(1000);
         _x++;
         Console.WriteLine(_x);
      }).Start();
      new Thread(() =>
      {
         Thread.Sleep(2000);
         _x++;
         Console.WriteLine(_x);
      }).Start();
      new Thread(() =>
      {
         Thread.Sleep(3000);
         _x++;
         Console.WriteLine(_x);
      }).Start();
   }
}