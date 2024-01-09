namespace AtomicityPattern;

public class Accumulator
{
   public int Total { get; private set; }

   public void Add(params int[] ints)
   {
      var success = false;
      var totalSnapshot = Total;
      try
      {
         foreach (var i in ints)
         {
            checked
            {
               Total += i;
            }
         }

         success = true;
      }
      finally
      {
         if (!success)
         {
            Total = totalSnapshot;
         }
      }
   }
}