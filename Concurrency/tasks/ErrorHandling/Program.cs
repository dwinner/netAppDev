namespace ErrorHandling;

internal static class Program
{
   private static void Main()
   {
      Flattening();
      SelectiveHandling();

      Console.ReadLine();
   }

   private static void Flattening()
   {
      try
      {
         var query = from i in ParallelEnumerable.Range(0, 1_000_000)
            select 100 / i;

         // Enumerate query
         query.ForAll(Console.Write);
      }
      catch (AggregateException aex)
      {
         foreach (var ex in aex.Flatten().InnerExceptions)
         {
            Console.WriteLine(ex.Message);
         }
      }
   }

   private static void SelectiveHandling()
   {
      var parent = Task.Factory.StartNew(() =>
      {
         // We’ll throw 3 exceptions at once using 3 child tasks:
         int[] numbers = { 0 };
         var childFactory = new TaskFactory
            (TaskCreationOptions.AttachedToParent, TaskContinuationOptions.None);

         childFactory.StartNew(() => 5 / numbers[0]); // Division by zero
         childFactory.StartNew(() => numbers[1]); // Index out of range
         childFactory.StartNew(() => throw null!); // Null reference
      });

      try
      {
         parent.Wait();
      }
      catch (AggregateException aex)
      {
         aex.Flatten().Handle(ex => // Note that we still need to call Flatten
         {
            switch (ex)
            {
               case DivideByZeroException:
                  Console.WriteLine("Divide by zero");
                  return true; // This exception is "handled"
               case IndexOutOfRangeException:
                  Console.WriteLine("Index out of range");
                  return true; // This exception is "handled"
               default:
                  return false; // All other exceptions will get rethrown
            }
         });
      }
   }
}