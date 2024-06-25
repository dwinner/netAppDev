using System.Collections.Generic;

namespace PsIterating
{
   public class CustomerRepository
   {
      [IterateAspect]
      public IEnumerable<string> GetAllNames()
      {
         yield return "Sam's Autobody";
         yield return "Trucking Aggregates";
         yield return "Multi-thread Sewing";
      }
   }
}