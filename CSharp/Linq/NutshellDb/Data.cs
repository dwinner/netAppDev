using System.Collections.Generic;

namespace NutshellDb
{
   public sealed class Customer
   {
      public int Id { get; set; }

      public string Name { get; set; }

      public ISet<Purchase> Purchases { get; set; }
   }

   public sealed class Purchase
   {
      public int Id { get; set; }

      public int CustomerId { get; set; }

      public decimal Price { get; set; }

      public string Description { get; set; }
   }
}