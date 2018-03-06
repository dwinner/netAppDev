using System.Collections.Generic;

namespace MainExample
{
   public class Vendor
   {
      public int Id { get; private set; }
      public string Name { get; private set; }

      public static IEnumerable<Vendor> GetVendors()
      {
         var vendors = new List<Vendor>
         {
            new Vendor {Name = "Sams Seaside Emporium", Id = 1},
            new Vendor {Name = "Jacks Guitar World", Id = 2},
            new Vendor {Name = "Thomas's Geek-O-Rama", Id = 3}
         };

         return vendors;
      }
   }
}