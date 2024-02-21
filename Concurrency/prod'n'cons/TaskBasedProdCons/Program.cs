namespace TaskBasedProdCons;

internal static class Program
{
   private static void Main()
   {
      using (var pcQ = new PcQueue(1))
      {
         var task1 = pcQ.EnqueueAsync(() => Console.WriteLine("Too"));
         var task2 = pcQ.EnqueueAsync(() => Console.WriteLine("Easy!"));

         task1.ContinueWith(_ => Console.WriteLine("Task 1 complete"));
         task2.ContinueWith(_ => Console.WriteLine("Task 2 complete"));
      }

      Console.ReadLine();
   }
}