namespace SelectMany_OuterJoin;

public class Customer
{
   public ISet<Purchase> Purchases { get; set; }
}

public class Purchase
{
   public string Description { get; set; }

   public decimal Price { get; set; }
}