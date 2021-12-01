using System.Collections.Generic;

namespace ValidationInCollectionSample
{
   public class Customer
   {
      public List<Order> Orders { get; set; } = new List<Order>();
   }
}