using System.Collections.Generic;

namespace HeavyBoxing
{
   internal static class Program
   {
      private static void Main()
      {
         var collection = new List<object>();
         while (true)
         {
            if (collection.Count > 100_000)
            {
               collection.Clear();
            }

            collection.Add(13);
         }
      }
   }
}