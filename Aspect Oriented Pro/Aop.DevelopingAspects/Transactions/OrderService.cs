namespace AopTransactions
{
   using Data;

   public static class OrderService
   {
      [RequiresTransaction]
      public static void PlaceOrder(string description, decimal amount)
      {
         using (var orderDb = new OrderDb())
         {
            orderDb.Orders.Add(new Order
            {
               Description = description,
               TotalAmount = amount
            });
            orderDb.SaveChanges();
         }
      }
   }
}