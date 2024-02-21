namespace ProdConsQueue;

internal static class Program
{
   private static void Main()
   {
      using var q = new PcQueue(1);
      q.EnqueueTask(() => Console.WriteLine("Foo"));
      q.EnqueueTask(() => Console.WriteLine("Far"));

      Console.ReadLine();
   }
}