namespace _07_Closures
{
   public class Processor
   {
      public ProcStrategy Strategy { get; set; }

      public int[] Process(int[] array)
      {
         var result = new int[array.Length];
         for (var i = 0; i < array.Length; i++)
         {
            result[i] = Strategy(array[i]);
         }

         return result;
      }
   }
}